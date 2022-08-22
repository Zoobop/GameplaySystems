using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    public class Follower : MovementState
    {
        private readonly ICharacterMovement _movement;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly ICharacter _leader;

        public Follower(ICharacterMovement movement, in NavMeshAgent navMeshAgent, ICharacter leader, IAnimator animator, in Transform orientation, in Rigidbody rigidbody) 
            : base(animator, in orientation, in rigidbody)
        {
            _movement = movement;
            _navMeshAgent = navMeshAgent;
            _leader = leader;
        }

        public override void OnEnter()
        {
            _rigidbody.isKinematic = false;
            _navMeshAgent.enabled = true;

            _movement.FollowTarget(_leader);
        }

        public override void OnTick()
        {
        }

        public override void OnFixedTick()
        {
        }

        public override void OnExit()
        {
            _movement.StopFollowing();

            _navMeshAgent.enabled = false;
            _rigidbody.isKinematic = true;
        }
    }
}