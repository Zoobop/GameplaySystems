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
        
        private void Start()
        {
            InputController.Instance.OnKeyBindsChanged += OnKeyBindsChangedCallback;

            // Initial Update
            OnKeyBindsChangedCallback(InputController.Instance.CurrentKeyBinds);
        }

        private void OnKeyBindsChangedCallback(KeyBindings keyBindings)
        {
            if (_inputType == InputType.Keyboard)
            {
                _moveForward.AssignBinding(MoveAction, keyBindings.MoveForwardKeyBind, _inputType);
                _moveBack.AssignBinding(MoveAction, keyBindings.MoveBackKeyBind, _inputType);
                _moveLeft.AssignBinding(MoveAction, keyBindings.MoveLeftKeyBind, _inputType);
                _moveRight.AssignBinding(MoveAction, keyBindings.MoveRightKeyBind, _inputType);
            }
            else // Gamepad
            {
                _movement.AssignBinding(MoveAction, keyBindings.MovementKeyBind, _inputType);
            }

            _sprint.AssignBinding(SprintAction, keyBindings.SprintKeyBind, _inputType);
            
            _interact.AssignBinding(InteractAction, keyBindings.InteractKeyBind, _inputType);

            _pauseMenu.AssignBinding(PauseMenuAction, keyBindings.OpenPauseMenuKeyBind, _inputType);
            _tabLeft.AssignBinding(TabLeftAction, keyBindings.TabLeftKeyBind, _inputType);
            _tabRight.AssignBinding(TabRightAction, keyBindings.TabRightKeyBind, _inputType);
            
            /*_playerProfile.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenPlayerProfileKeyBind)),
                "Player Profile", keyBindings.OpenPlayerProfileKeyBind, _inputType);
            _inventory.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenInventoryKeyBind)), "Inventory",
                keyBindings.OpenInventoryKeyBind, _inputType);
            _skillTree.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenSkillTreeKeyBind)), "Skill Tree",
                keyBindings.OpenSkillTreeKeyBind, _inputType);
            _settings.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenSettingsKeyBind)), "Settings",
                keyBindings.OpenSettingsKeyBind, _inputType);*/
        }
    }
}
