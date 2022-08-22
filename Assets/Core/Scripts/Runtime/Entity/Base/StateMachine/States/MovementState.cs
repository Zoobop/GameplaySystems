using UnityEngine;

namespace Entity
{
    public abstract class MovementState : IState
    {
        protected readonly IAnimator _animator;
        protected readonly Transform _orientation;
        protected readonly Rigidbody _rigidbody;

        public MovementState(IAnimator animator, in Transform orientation, in Rigidbody rigidbody)
        {
            _animator = animator;
            _orientation = orientation;
            _rigidbody = rigidbody;
        }

        #region IState

        public abstract void OnEnter();

        public abstract void OnTick();

        public abstract void OnFixedTick();

        public abstract void OnExit();

        #endregion
    }
}