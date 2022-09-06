using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    public abstract class Movement : MonoBehaviour, IMovement
    {
        [Header("Movement Details")] 
        [Min(0)] [SerializeField] protected float _movementSpeed = 4f;

        protected MovementState _movementState;
        protected Rigidbody _rigidbody;
        protected IAnimator _animator;
        protected Transform _orientation;
        protected NavMeshAgent _navMeshAgent;

        // Movement
        private bool _isMoving;

        #region UnityEvents

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<IAnimator>();
            //_navMeshAgent = GetComponent<NavMeshAgent>();
            _orientation = transform.Find("Orientation");
        }

        protected void Update()
        {
            _movementState?.OnTick();
        }

        protected void FixedUpdate()
        {
            _movementState?.OnFixedTick();
        }

        #endregion

        #region IMovement

        public MovementState GetMovementState()
        {
            return _movementState;
        }

        public void SetMovementState(in MovementState state)
        {
            _movementState = state;
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return _navMeshAgent;
        }

        public float GetMovementSpeed()
        {
            return _movementSpeed;
        }

        public void SetMoving(bool state)
        {
            _isMoving = state;
        }

        public bool IsMoving()
        {
            return _isMoving;
        }

        #endregion
    }
}
