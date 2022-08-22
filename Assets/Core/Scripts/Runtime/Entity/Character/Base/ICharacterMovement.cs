

using UnityEngine;

namespace Entity
{
    public interface ICharacterMovement : IMovement
    {
        public void ToggleSprint();
        public float GetSprintModifier();
        public bool IsSprinting();

        public void FollowTarget(ICharacter target);
        public void StopFollowing();
    }
}