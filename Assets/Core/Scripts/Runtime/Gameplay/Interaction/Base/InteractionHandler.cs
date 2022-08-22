using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InteractionSystem
{

    using Entity;
    using InputSystem;
    using UI;
    using Utility.ExtensionMethods;

    public class InteractionHandler : InteractionTarget, IInteractionInitiator
    {
        private const float ControlInteractRadius = 2f;
        private const float DefaultInteractRadius = 0.75f;

        [Header("Interaction Details")] [SerializeField, Min(0.1f)]
        private float _interactionRadius = 2f;

        [SerializeField, Min(0.0001f)] private float _tickSpeed = 0.0001f;
        [SerializeField] private bool _isInteractionInitiator;

        private InteractContentWindow _interactContentWindow;
        private SphereCollider _collider;
        private Coroutine _interactionCoroutine;
        private WaitForSeconds _tickDelay;
        private InputAction _interactionAction;
        private bool _isInteracting;

        private readonly IList<InteractionTarget> _interactionTargets = new List<InteractionTarget>();
        private IInteractionTarget _priorityTarget;
        private IInteractionInitiator _interactionInitiatorImplementation;

        #region UnityEvents

        protected override void Awake()
        {
            // Parent implementation
            base.Awake();

            GameManager.OnPlayerChanged += OnPlayerChangedCallback;

            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = _interactionRadius;
            _tickDelay = new WaitForSeconds(_tickSpeed);
        }

        private void Start()
        {
            _interactContentWindow = InteractContentWindow.Instance;
            _interactionAction = InputController.Instance.GetPlayerInputAction("Interact");
        }

        protected override void OnValidate()
        {
            // Parent implementation
            base.OnValidate();

            // Assign if null
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = _interactionRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Return if not initiator
            if (!_isInteractionInitiator) return;

            // Try get interactable
            if (other.TryGetComponent<InteractionTarget>(out var target))
            {
                if (!target.HasInteractions()) return;

                // Add interactable
                if (_interactionTargets.TryAdd(target))
                {
                    // Priority check
                    Detect();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Return if not initiator
            if (!_isInteractionInitiator) return;

            // Try get interactable
            if (other.TryGetComponent<InteractionTarget>(out var target))
            {
                if (!target.HasInteractions()) return;

                // Remove interactable
                _interactionTargets.Remove(target);

                if (_interactionTargets.IsEmpty())
                {
                    // Nullify priority target
                    _priorityTarget = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_interactionTargets.IsEmpty() || _isInteracting || _priorityTarget == null) return;

            var priorityTarget = _priorityTarget as InteractionTarget;
            if (priorityTarget == null) return;

            Gizmos.color = Color.green;
            var position = priorityTarget.transform.position;
            Gizmos.DrawLine(transform.position, position);

            Gizmos.color = Color.red;
            foreach (var target in _interactionTargets)
            {
                if (target == priorityTarget) continue;

                var pos = target.transform.position;
                Gizmos.DrawLine(transform.position, pos);
            }
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _isInteractionInitiator = player == GetComponentInParent<ICharacter>();
            _interactionRadius = _isInteractionInitiator ? ControlInteractRadius : DefaultInteractRadius;
            _collider.radius = _interactionRadius;
        }

        #region TargetDetection

        private void Detect()
        {
            if (_interactionCoroutine != null)
            {
                StopCoroutine(CheckForInteractableEntities());
            }

            _interactionCoroutine = StartCoroutine(CheckForInteractableEntities());
        }

        private IEnumerator CheckForInteractableEntities()
        {
            // Check for closest entity
            while (!_interactionTargets.IsEmpty())
            {
                UpdatePriorityList();
                CheckInteractability();
                ShowWindow();
                GetInteractInput();

                // While interacting...
                while (_isInteracting)
                {
                    yield return _tickDelay;
                }

                yield return _tickDelay;
            }

            // Close window
            _interactContentWindow.CloseWindow();
        }

        #endregion

        #region Helpers

        private void UpdatePriorityList()
        {
            if (_interactionTargets.IsEmpty()) return;

            // Calculate closest interactable target
            var index = 0;
            var interactorPosition = transform.position;
            var minDistance = _interactionRadius * _interactionRadius;

            var targets = _interactionTargets.ToArray();
            for (var i = 0; i < targets.Length; i++)
            {
                var targetPosition = targets[i].transform.position;
                var distanceAway = interactorPosition.Distance(targetPosition);
                if (distanceAway < minDistance)
                {
                    minDistance = distanceAway;
                    index = i;
                }
            }

            // Set priority target if changed
            if (!ReferenceEquals(_priorityTarget, _interactionTargets[index]))
            {
                _priorityTarget = _interactionTargets[index];
            }
        }

        private void CheckInteractability()
        {
            if (_interactionTargets.IsEmpty()) return;

            // Check for active interactions
            if (!_priorityTarget.HasInteractions())
            {
                _interactionTargets.Remove(_priorityTarget as InteractionTarget);
                _priorityTarget = null;
                _interactContentWindow.CloseWindow();
                UpdatePriorityList();
            }
        }

        private void ShowWindow()
        {
            if (_interactionTargets.IsEmpty()) return;

            // Open window if closed
            if (!_interactContentWindow.IsWindowOpen())
            {
                _interactContentWindow.OpenWindow(_priorityTarget);
                return;
            }

            // Set target
            _interactContentWindow.SetTarget(_priorityTarget);
        }

        private void GetInteractInput()
        {
            // Interact on pressed
            if (_interactionAction.IsPressed() && !_isInteracting && !_interactionTargets.IsEmpty())
            {
                StartInteracting();
                // Automatically start interaction if only one
                var interactions = _priorityTarget.GetInteractions().ToList();
                if (_priorityTarget.ActiveInteractionsCount() == 1)
                {
                    InteractionController.Instance.StartInteraction(interactions[0]);
                    _interactContentWindow.CloseWindow();
                    return;
                }

                // Else show all available interaction options
                _interactContentWindow.ShowInteractions(interactions);
            }
        }

        #endregion

        #region IInteractionInitiator

        public void StartInteracting()
        {
            _isInteracting = true;
        }

        public void StopInteracting()
        {
            _isInteracting = false;
        }

        public bool IsInteracting()
        {
            return _isInteracting;
        }

        #endregion
    }
}