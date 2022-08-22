using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    using Groups;
    
    public class CharacterMovement : Movement, ICharacterMovement
    {
        [Header("Sprint Details")] 
        [Min(0)] [SerializeField] private float _sprintModifier = 1.2f;
        [Min(0.1f)] [SerializeField] private float _staminaTickSpeed = 0.2f;
        [Min(1)] [SerializeField] private float _staminaDrainPerTick = 5f;
        [Min(0.1f)] [SerializeField] private float _staminaRecoveryPerTick = 1f;
        [Min(0.1f)] [SerializeField] private float _staminaRecoveryDelay = 1f;

        [Header("AI Navigation")] 
        [SerializeField, Min(0f)] private float _stoppingDistance = 1.5f;
        [SerializeField, Min(0.0001f)] private float _aiTickSpeed = 0.2f;

        private IStats _stats;
        private WaitForSeconds _recoveryDelay;
        private WaitForSeconds _staminaTickDelay;
        private Coroutine _sprintCoroutine;

        private WaitForSeconds _aiTickDelay;
        private Coroutine _followCoroutine;

        private bool _isSprinting;

        #region UnityEvents

        protected override void Awake()
        {
            // Parent implementation
            base.Awake();

            PlayerPartyController.OnPartyChanged += OnPartyChangedCallback;

            // Controlled movement
            _stats = GetComponent<ICharacter>().GetStats();
            _recoveryDelay = new WaitForSeconds(_staminaRecoveryDelay);
            _staminaTickDelay = new WaitForSeconds(_staminaTickSpeed);
            
            // AI Movement
            _aiTickDelay = new WaitForSeconds(_aiTickSpeed);
        }

        private void OnValidate()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.speed = _movementSpeed;
            _navMeshAgent.stoppingDistance = _stoppingDistance;
        }

        #endregion

        private void OnPartyChangedCallback(PlayerParty party)
        {
            UpdateStates(party.PartyLeader);
        }

        private void UpdateStates(ICharacter leader)
        {
            var playerPartyController = PlayerPartyController.Instance;
            var character = GetComponent<ICharacter>();
            
            _movementState?.OnExit();
            if (character == leader)
            {
                _movementState = new CharacterControl(this, _navMeshAgent, _animator, _orientation, _rigidbody);
            }
            /*else if (playerPartyController.IsActivePartyMember(character))
            {
                _movementState = new Follower(this, _navMeshAgent, leader, _animator, _orientation, _rigidbody);
            }*/
            else
            {
                _movementState = new Idle(_animator, _orientation, _rigidbody);
            }
            _movementState?.OnEnter();
        }

        #region ICharacterMovement

        public float GetSprintModifier()
        {
            return _sprintModifier;
        }

        public bool IsSprinting()
        {
            return _isSprinting;
        }

        public void FollowTarget(ICharacter target)
        {
            _followCoroutine = StartCoroutine(Follow(target));
        }

        public void StopFollowing()
        {
            if (_followCoroutine != null)
            {
                StopCoroutine(_followCoroutine);
            }
        }

        public void ToggleSprint()
        {
            // Return if not moving
            if (!IsMoving()) return;

            // Stop running coroutine
            if (_sprintCoroutine != null)
            {
                StopCoroutine(_sprintCoroutine);
            }

            // Check if sprinting
            if (!_isSprinting) // Drain
            {
                // Start sprinting
                _isSprinting = true;
                _sprintCoroutine = StartCoroutine(DrainStamina());
            }
            else // Recovery
            {
                // Start sprinting
                _isSprinting = false;
                _sprintCoroutine = StartCoroutine(RecoverStamina());
            }
        }

        #endregion

        #region Sprinting

        private IEnumerator DrainStamina()
        {
            // Drain stamina
            var stat = _stats.GetStat(StatAttributeType.Stamina);
            while (IsMoving() && _isSprinting && stat.Current > stat.Minimum)
            {
                _stats.ModifyStat(StatAttributeType.Stamina, (int) -_staminaDrainPerTick);
                yield return _staminaTickDelay;
            }

            // Stop sprinting
            _isSprinting = false;

            // Start Recovery
            _sprintCoroutine = StartCoroutine(RecoverStamina());
        }

        private IEnumerator RecoverStamina()
        {
            // Recovery delay
            yield return _recoveryDelay;

            // Recover stamina
            var stat = _stats.GetStat(StatAttributeType.Stamina);
            while (stat.Current < stat.Maximum)
            {
                _stats.ModifyStat(StatAttributeType.Stamina, (int) _staminaRecoveryPerTick);
                yield return _staminaTickDelay;
            }
        }

        #endregion

        #region AI

        private IEnumerator Follow(IEntity target)
        {
            while (target != null)
            {
                var targetPosition = target.GetTransform().position;
                _navMeshAgent.SetDestination(targetPosition);

                yield return _aiTickDelay;
            }
        }

        #endregion
    }
}