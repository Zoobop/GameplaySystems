using UnityEngine;

namespace Entity
{
    public class Idle : MovementState
    {
        public Idle(IAnimator animator, in Transform orientation, in Rigidbody rigidbody)
            : base(animator, orientation, rigidbody)
        {
        }

        public override void OnEnter()
        {
            _rigidbody.mass = 10000f;
            _rigidbody.isKinematic = true;
        }

        public override void OnTick()
        {

        }

        public override void OnFixedTick()
        {

        }

        public override void OnExit()
        {
            _rigidbody.isKinematic = false;
        }
    }
}