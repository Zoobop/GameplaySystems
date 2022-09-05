using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

using InputSystem;

namespace Entity
{
    public class CharacterControl : MovementState
    {
        private readonly ICharacterMovement _characterMovement;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly InputAction _movementAction;
        private readonly float _movementSpeed;
        private readonly float _sprintModifier;

        private Vector2 _movementVector;
        private float _currentSprintMod;

        public CharacterControl(ICharacterMovement movement, NavMeshAgent navMeshAgent, IAnimator animator, in Transform orientation,
            in Rigidbody rigidbody)
            : base(animator, orientation, rigidbody)
        {
            _characterMovement = movement;
            _navMeshAgent = navMeshAgent;
            _movementAction = InputController.GetPlayerInputAction("Move");
            _movementSpeed = movement.GetMovementSpeed();
            _sprintModifier = movement.GetSprintModifier();
        }

        #region IState

        public override void OnEnter()
        {
            InputController.SetCallback("Sprint", ToggleSprint);
            _rigidbody.mass = 1f;
            _rigidbody.isKinematic = false;
            _navMeshAgent.enabled = false;
        }

        public override void OnTick()
        {
            // Assign movement values
            _movementVector = _movementAction.ReadValue<Vector2>();
            _characterMovement.SetMoving(_movementVector.magnitude >= 0.5f);

            //Debug.Log($"Move: {_characterMovement.IsMoving()} - Sprint: {_characterMovement.IsSprinting()}");

            // Get sprint modifier if can sprint
            _currentSprintMod = GetModifierValue();

            // Apply to animator
            var speedPercent = _rigidbody.velocity.magnitude / (_movementSpeed * _sprintModifier);
            _animator.SetAnimationBlend(speedPercent, Time.deltaTime);
        }

        public override void OnFixedTick()
        {
            // Calculate move direction
            var moveDirection = _orientation.forward * _movementVector.y + _orientation.right * _movementVector.x;

            // Move character
            var gravity = _orientation.up * Mathf.Min(_rigidbody.velocity.y, 0f);
            _rigidbody.velocity = moveDirection * (_movementSpeed * _currentSprintMod) + gravity;
        }

        public override void OnExit()
        {
            _rigidbody.isKinematic = true;
            _navMeshAgent.enabled = true;
            InputController.RemoveCallback("Sprint", ToggleSprint);
        }

        #endregion

        private float GetModifierValue()
        {
            return _characterMovement.IsMoving() && _characterMovement.IsSprinting() ? _sprintModifier : 1f;
        }

        private void ToggleSprint(InputAction.CallbackContext context)
        {
            _characterMovement.ToggleSprint();
        }
    }
}