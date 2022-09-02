using UnityEngine;

using InputSystem;

namespace UI
{
    public class KeyBindingsUI : MonoBehaviour
    {
        [Header("Input Type")] [SerializeField]
        private InputType _inputType;

        // Player
        [Header("Interaction")] [SerializeField]
        private RebindKeyUI _interact;

        // Player UI
        [Header("Player UI")] 
        [SerializeField] private RebindKeyUI _playerProfile;
        [SerializeField] private RebindKeyUI _inventory;
        [SerializeField] private RebindKeyUI _skillTree;
        [SerializeField] private RebindKeyUI _settings;
        [SerializeField] private RebindKeyUI _pauseMenu;

        private void Start()
        {
            InputController.Instance.OnKeyBindsChanged += OnKeyBindsChangedCallback;

            // Initial Update
            OnKeyBindsChangedCallback(InputController.Instance.CurrentKeyBinds);
        }

        private void OnKeyBindsChangedCallback(KeyBindings keyBindings)
        {
            //_moveUp.AssignBinding("Move", KeyBindings.GetActionName(nameof(keyBindings.MoveUpKeyBind)), keyBindings.MoveUpKeyBind, 1);
            //_moveDown.AssignBinding("Move", KeyBindings.GetActionName(nameof(keyBindings.MoveDownKeyBind)), keyBindings.MoveDownKeyBind, 2);
            //_moveLeft.AssignBinding("Move", KeyBindings.GetActionName(nameof(keyBindings.MoveLeftKeyBind)), keyBindings.MoveLeftKeyBind, 3);
            //_moveRight.AssignBinding("Move", KeyBindings.GetActionName(nameof(keyBindings.MoveRightKeyBind)), keyBindings.MoveRightKeyBind, 4);
            _interact.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.InteractKeyBind)),
                KeyBindings.GetActionName(nameof(keyBindings.InteractKeyBind)), keyBindings.InteractKeyBind,
                _inputType);

            _playerProfile.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenPlayerProfileKeyBind)),
                "Player Profile", keyBindings.OpenPlayerProfileKeyBind, _inputType);
            _inventory.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenInventoryKeyBind)), "Inventory",
                keyBindings.OpenInventoryKeyBind, _inputType);
            _skillTree.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenSkillTreeKeyBind)), "Skill Tree",
                keyBindings.OpenSkillTreeKeyBind, _inputType);
            _settings.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenSettingsKeyBind)), "Settings",
                keyBindings.OpenSettingsKeyBind, _inputType);
            _pauseMenu.AssignBinding(KeyBindings.GetActionName(nameof(keyBindings.OpenPauseMenuKeyBind)), "Pause Menu",
                keyBindings.OpenPauseMenuKeyBind, _inputType);
        }
    }
}
