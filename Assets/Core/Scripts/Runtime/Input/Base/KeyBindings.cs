using System;
using UnityEngine;

namespace InputSystem
{

    [Serializable]
    public class KeyBindings
    {
        [Header("Player")] [SerializeField] private KeyBind _interactKeyBind = KeyBind.E;

        [Header("Player UI")] [SerializeField] private KeyBind _playerProfileKeyBind = KeyBind.U;
        [SerializeField] private KeyBind _inventoryKeyBind = KeyBind.I;
        [SerializeField] private KeyBind _skillTreeKeyBind = KeyBind.O;
        [SerializeField] private KeyBind _settingsKeyBind = KeyBind.P;
        [SerializeField] private KeyBind _pauseMenuKeyBind = KeyBind.Escape;

        [Header("Tab Control")] [SerializeField]
        private KeyBind _tabLeft = KeyBind.Q;

        [SerializeField] private KeyBind _tabRight = KeyBind.E;

        public KeyBind this[string bind]
        {
            get => GetKeyBindByName(bind);
            set => SetKeyBindByName(bind, value);
        }

        // Player
        public KeyBind InteractKeyBind
        {
            get => _interactKeyBind;
            set => _interactKeyBind = value;
        }

        // Player UI
        public KeyBind OpenPlayerProfileKeyBind
        {
            get => _playerProfileKeyBind;
            set => _playerProfileKeyBind = value;
        }

        public KeyBind OpenInventoryKeyBind
        {
            get => _inventoryKeyBind;
            set => _inventoryKeyBind = value;
        }

        public KeyBind OpenSkillTreeKeyBind
        {
            get => _skillTreeKeyBind;
            set => _skillTreeKeyBind = value;
        }

        public KeyBind OpenSettingsKeyBind
        {
            get => _settingsKeyBind;
            set => _settingsKeyBind = value;
        }

        public KeyBind OpenPauseMenuKeyBind
        {
            get => _pauseMenuKeyBind;
            set => _pauseMenuKeyBind = value;
        }

        // Tab Control
        public KeyBind TabLeft
        {
            get => _tabLeft;
            set => _tabLeft = value;
        }

        public KeyBind TabRight
        {
            get => _tabRight;
            set => _tabRight = value;
        }

        public static string GetActionName(string actionName)
        {
            var index = actionName.LastIndexOf('K');
            return actionName.Remove(index);
        }

        public KeyBind GetKeyBindByName(string bindName)
        {
            return bindName switch
            {
                // Player
                "Interact" => _interactKeyBind,
                // Player UI
                "OpenPlayerProfile" => _playerProfileKeyBind,
                "OpenInventory" => _inventoryKeyBind,
                "OpenSkillTree" => _skillTreeKeyBind,
                "OpenSettings" => _settingsKeyBind,
                "OpenPauseMenu" => _pauseMenuKeyBind,
                // Tab Control
                "TabLeft" => _tabLeft,
                "TabRight" => _tabRight,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetKeyBindByName(string bindName, KeyBind key)
        {
            // Player
            if (bindName == "Interact") _interactKeyBind = key;

            // Player UI
            else if (bindName == "OpenPlayerProfile") _playerProfileKeyBind = key;
            else if (bindName == "OpenInventory") _inventoryKeyBind = key;
            else if (bindName == "OpenSkillTree") _skillTreeKeyBind = key;
            else if (bindName == "OpenSettings") _settingsKeyBind = key;
            else if (bindName == "OpenPauseMenu") _pauseMenuKeyBind = key;

            // Tab Control
            else if (bindName == "TabLeft") _tabLeft = key;
            else if (bindName == "TabRight") _tabRight = key;
        }
    }
}
