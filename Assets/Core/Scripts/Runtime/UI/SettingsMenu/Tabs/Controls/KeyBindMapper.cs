using System;
using UnityEngine;

using InputSystem;

namespace UI
{
    public class KeyBindMapper : MonoBehaviour
    {
        /* Input */
        [SerializeField] private InputType _inputType;

        /* Movement */
        [SerializeField] private KeyBindUI _moveForward;
        [SerializeField] private KeyBindUI _moveBack;
        [SerializeField] private KeyBindUI _moveLeft;
        [SerializeField] private KeyBindUI _moveRight;
        [SerializeField] private KeyBindUI _movement;
        [SerializeField] private KeyBindUI _sprint;
        
        /* Interaction */
        [SerializeField] private KeyBindUI _interact;

        /* UI */
        [SerializeField] private KeyBindUI _pauseMenu;
        [SerializeField] private KeyBindUI _tabLeft;
        [SerializeField] private KeyBindUI _tabRight;

        private const string MoveAction = "Move";
        private const string SprintAction = "Sprint";
        private const string InteractAction = "Interact";
        
        private const string PauseMenuAction = "OpenPauseMenu";
        private const string TabLeftAction = "TabLeft";
        private const string TabRightAction = "TabRight";

        #region UnityEvents

        private void Start()
        {
            InputController.OnKeyBindsChanged += OnKeyBindsChangedCallback;

            // Initial Update
            OnKeyBindsChangedCallback(InputController.CurrentKeyBinds);
        }

        #endregion
        
        private void OnKeyBindsChangedCallback(KeyBindings keyBindings)
        {
            if (_inputType == InputType.Keyboard)
            {
                _moveForward.AssignBinding(MoveAction, "MoveForward", keyBindings.MoveForwardKeyBind, _inputType, 1);
                _moveBack.AssignBinding(MoveAction, "MoveBack", keyBindings.MoveBackKeyBind, _inputType, 2);
                _moveLeft.AssignBinding(MoveAction, "MoveLeft", keyBindings.MoveLeftKeyBind, _inputType, 3);
                _moveRight.AssignBinding(MoveAction, "MoveRight", keyBindings.MoveRightKeyBind, _inputType, 4);
            }
            else // Gamepad
            {
                _movement.AssignBinding(MoveAction, "Movement", keyBindings.MovementKeyBind, _inputType, 4);
            }

            _sprint.AssignBinding(SprintAction, keyBindings.SprintKeyBind, _inputType);
            
            _interact.AssignBinding(InteractAction, keyBindings.InteractKeyBind, _inputType);

            _pauseMenu.AssignBinding(PauseMenuAction, keyBindings.OpenPauseMenuKeyBind, _inputType);
            _tabLeft.AssignBinding(TabLeftAction, keyBindings.TabLeftKeyBind, _inputType);
            _tabRight.AssignBinding(TabRightAction, keyBindings.TabRightKeyBind, _inputType);
        }

        public void ResetAllToDefault()
        {
            if (_inputType == InputType.Keyboard)
            {
                _moveForward.ResetKeyBind();
                _moveBack.ResetKeyBind();
                _moveLeft.ResetKeyBind();
                _moveRight.ResetKeyBind();
            }
            else // Gamepad
            {
                _movement.ResetKeyBind();
            }

            _sprint.ResetKeyBind();
            _interact.ResetKeyBind();
            _pauseMenu.ResetKeyBind();
            _tabLeft.ResetKeyBind();
            _tabRight.ResetKeyBind();
        }
    }
}
