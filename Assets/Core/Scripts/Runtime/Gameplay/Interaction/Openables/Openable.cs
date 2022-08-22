using System.Collections;
using UnityEngine;

namespace InteractionSystem
{

    using Entity;
    using UI;

    public abstract class Openable : MonoBehaviour, IOpenable
    {
        private enum Axis
        {
            X,
            Y,
            Z
        }

        [Header("General")] [SerializeField] protected string _name;

        [Header("References")] [SerializeField]
        protected Transform _openable;

        [SerializeField] protected Transform _hinge;

        [Header("Options")]
        [Tooltip(
            "Does the openable object open based on the iteractor's forward position? (i.e. If true, a door will always open away from the interactor.)")]
        [SerializeField]
        private bool _isDirectionBased;

        [Tooltip("Is the openable currently open?")] [SerializeField]
        private bool _isOpen;

        [Tooltip("Is the openable currently locked?")] [SerializeField]
        private bool _isLocked;

        [Tooltip(
            "Does the interactor always have to meet the open condition? (i.e. If true, the interactor would have to have the key in hand every time to open the door.)")]
        [SerializeField]
        private bool _isConditionAlwaysActive;

        [Header("Open Rotation Configuration")] [SerializeField]
        private Axis _axisOfRotation;

        [Min(0)] [SerializeField] private float _rotationAmount;
        [Min(0)] [SerializeField] private float _rotationTime;
        [Range(0, 1)] [SerializeField] private float _currentRotationAmount;

        private Transform _debugHinge;
        private Vector3 _startingRotation;
        private Vector3 _endingRotation;
        private Vector3 _forward;

        protected ICharacter _player;
        protected Coroutine _animationCoroutine;
        protected Collider _openableCollider;
        protected IUnlockCondition _unlockCondition;

        #region UnityEvents

        protected virtual void Awake()
        {
            GameManager.OnPlayerChanged += OnPlayerChangedCallback;

            _openableCollider = _openable.GetComponent<Collider>();
            _unlockCondition = GetComponent<IUnlockCondition>();

            _debugHinge = _hinge.parent.Find("DebugHingeEndpoint");
            _startingRotation = _hinge.rotation.eulerAngles;
            _endingRotation = _debugHinge.rotation.eulerAngles;
            _forward = _hinge.forward;
        }

        protected virtual void OnValidate()
        {
            // Warn if null
            if (_hinge is null)
            {
                Debug.LogWarning("Hinge has not been set!");
                return;
            }
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _player = player;
        }

        #region IOpenable

        public virtual void Toggle(Vector3 position = new())
        {
            // Check open status
            if (!_isOpen)
            {
                // Check lock condition
                if (!CheckCondition()) return;

                // Open
                OnOpen();
                Open(position);
            }
            else
            {
                // Close
                OnClose();
                Close();
            }
        }

        public void Open(Vector3 userPosition = new())
        {
            // Check for running coroutine
            if (_animationCoroutine is not null)
            {
                StopCoroutine(_animationCoroutine);
            }

            // Get direction of player to openable
            var dotProduct = Vector3.Dot(_forward, (userPosition - _hinge.position).normalized);
            // Start open animation (coroutine)
            _animationCoroutine = StartCoroutine(AnimateOpen(dotProduct));
        }

        public void Close()
        {
            // Check for running coroutine
            if (_animationCoroutine is not null)
            {
                StopCoroutine(_animationCoroutine);
            }

            // Start close animation (coroutine)
            _animationCoroutine = StartCoroutine(AnimateClose());
        }

        #endregion

        #region OpenableUtility

        protected abstract void OnOpen();

        protected abstract void OnClose();

        protected abstract void OnAnimationStart();

        protected abstract void OnAnimationEnd();

        protected virtual IEnumerator AnimateOpen(float direction)
        {
            // Invoke animation start
            OnAnimationStart();

            // Get start and end rotation
            var startRotation = _hinge.rotation;

            // Get rotation based on axis
            var endRotation = Quaternion.Euler(_endingRotation);
            /*var rotationIdentity = _axisOfRotation switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => Vector3.zero
            };
            
            var rotationVector = rotationIdentity * _rotationAmount;
    
            // Check if directional
            if (_isDirectionBased)
            {
                var inFront = direction >= 0;
                //print($"Is in front: {inFront} - Dot: {direction}");
                endRotation = inFront ? 
                    Quaternion.Euler(_startingRotation - rotationVector) : // Interactor in front
                    Quaternion.Euler(_startingRotation + rotationVector); // Interactor behind
            }
            else
            {
                endRotation = Quaternion.Euler(_startingRotation - rotationVector);
            }*/

            // Set open to true
            _isOpen = true;

            // Disable collision
            _openableCollider.enabled = false;

            // Animate smoothly
            var elapsedTime = 0f;
            while (elapsedTime <= _rotationTime)
            {
                elapsedTime += Time.smoothDeltaTime;
                _hinge.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / _rotationTime);
                yield return null;
            }

            // Re-enable collision
            _openableCollider.enabled = true;

            // Invoke animation end
            OnAnimationEnd();
        }

        protected virtual IEnumerator AnimateClose()
        {
            // Invoke animation start
            OnAnimationStart();

            // Get start and end rotation
            var startRotation = _hinge.rotation;
            var endRotation = Quaternion.Euler(_startingRotation);

            // Set open to false
            _isOpen = false;

            // Disable collision
            _openableCollider.enabled = false;

            // Animate smoothly
            var elapsedTime = 0f;
            while (elapsedTime <= _rotationTime)
            {
                elapsedTime += Time.smoothDeltaTime;
                _hinge.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / _rotationTime);
                yield return null;
            }

            // Re-enable collision
            _openableCollider.enabled = true;

            // Invoke animation end
            OnAnimationEnd();
        }

        #endregion

        #region UnlockCondition

        private bool CheckCondition()
        {
            // Check if there is a condition
            if (_unlockCondition is null) return true;

            // Store the result of the condition
            var result = _unlockCondition.IsConditionMet(_player);

            // One and done (unlock needed only once)
            if (!_isConditionAlwaysActive)
            {
                // Only if locked
                if (result && _isLocked)
                {
                    _isLocked = false;
                    var message = $"Unlocked {_name}!";
                    Debug.LogWarning(message);
                    GameEventLog.LogEvent(message);

                    return true;
                }
            }

            // Condition always has to meet
            return result;
        }

        #endregion

        #region IEntity

        public string GetName()
        {
            return _name;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Transform Spawn(Transform parent = null)
        {
            return transform;
        }

        public void Despawn(bool destroy = true)
        {

        }

        #endregion
    }
}
