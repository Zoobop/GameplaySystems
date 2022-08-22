
using UnityEngine.AI;

namespace Entity
{
    public interface IMovement
    {
        public MovementState GetMovementState();
        public void SetMovementState(in MovementState state);
        public NavMeshAgent GetNavMeshAgent();
        public float GetMovementSpeed();
        public void SetMoving(bool state);
        public bool IsMoving();
    }
}