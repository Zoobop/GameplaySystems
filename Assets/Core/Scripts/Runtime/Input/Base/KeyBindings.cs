using System;
using UnityEngine;

namespace InputSystem
{

    [Serializable]
    public class KeyBindings
    {
        [Header("Player")]
        [SerializeField] private KeyBind _moveForwardKeyBind = KeyBind.W;
        [SerializeField] private KeyBind _moveLeftKeyBind = KeyBind.A;
        [SerializeField] private KeyBind _moveBackKeyBind = KeyBind.S;
        [SerializeField] private KeyBind _moveRightKeyBind = KeyBind.D;
        [SerializeField] private KeyBind _movementKeyBind = KeyBind.GamepadLeftStick;
        [SerializeField] private KeyBind _sprintKeyBind = KeyBind.LeftShift;
        [SerializeField] private KeyBind _interactKeyBind = KeyBind.F;

        [Header("Player UI")] 
        [SerializeField] private KeyBind _playerProfileKeyBind = KeyBind.U;
        [SerializeField] private KeyBind _inventoryKeyBind = KeyBind.I;
        [SerializeField] private KeyBind _skillTreeKeyBind = KeyBind.O;
        [SerializeField] private KeyBind _settingsKeyBind = KeyBind.P;
        [SerializeField] private KeyBind _pauseMenuKeyBind = KeyBind.Escape;
        
        [Header("Tab Control")]
        [SerializeField] private KeyBind _tabLeftKeyBind = KeyBind.Q;
        [SerializeField] private KeyBind _tabRightKeyBind = KeyBind.E;
        
        public KeyBind this[string bind]
        {
            get => GetKeyBindByName(bind);
            set => SetKeyBindByName(bind, value);
        }

        // Player
        public KeyBind MoveForwardKeyBind
        {
            get => _moveForwardKeyBind;
            set => _moveForwardKeyBind = value;
        }
        
        public KeyBind MoveLeftKeyBind
        {
            get => _moveLeftKeyBind;
            set => _moveLeftKeyBind = value;
        }
        
        public KeyBind MoveBackKeyBind
        {
            get => _moveBackKeyBind;
            set => _moveBackKeyBind = value;
        }
        
        public KeyBind MoveRightKeyBind
        {
            get => _moveRightKeyBind;
            set => _moveRightKeyBind = value;
        }
        
        public KeyBind MovementKeyBind
        {
            get => _movementKeyBind;
            set => _movementKeyBind = value;
        }
        
        public KeyBind SprintKeyBind
        {
            get => _sprintKeyBind;
            set => _sprintKeyBind = value;
        }
        
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
        public KeyBind TabLeftKeyBind
        {
            get => _tabLeftKeyBind;
            set => _tabLeftKeyBind = value;
        }

        public KeyBind TabRightKeyBind
        {
            get => _tabRightKeyBind;
            set => _tabRightKeyBind = value;
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
                "MoveForward" => _moveForwardKeyBind,
                "MoveLeft" => _moveLeftKeyBind,
                "MoveBack" => _moveBackKeyBind,
                "MoveRight" => _moveRightKeyBind,
                "Movement" => _movementKeyBind,
                "Sprint" => _sprintKeyBind,
                "Interact" => _interactKeyBind,
                // Player UI
                "OpenPlayerProfile" => _playerProfileKeyBind,
                "OpenInventory" => _inventoryKeyBind,
                "OpenSkillTree" => _skillTreeKeyBind,
                "OpenSettings" => _settingsKeyBind,
                "OpenPauseMenu" => _pauseMenuKeyBind,
                // Tab Control
                "TabLeft" => _tabLeftKeyBind,
                "TabRight" => _tabRightKeyBind,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetKeyBindByName(string bindName, KeyBind key)
        {
            switch (bindName)
            {
                // Player
                case "MoveForward":
                    _moveForwardKeyBind = key;
                    break;
                case "MoveLeft":
                    _moveLeftKeyBind = key;
                    break;
                case "MoveBack":
                    _moveBackKeyBind = key;
                    break;
                case "MoveRight":
                    _moveRightKeyBind = key;
                    break;
                case "Movement":
                    _movementKeyBind = key;
                    break;
                case "Sprint":
                    _sprintKeyBind = key;
                    break;
                case "Interact":
                    _interactKeyBind = key;
                    break;
                // Player UI
                case "OpenPlayerProfile":
                    _playerProfileKeyBind = key;
                    break;
                case "OpenInventory":
                    _inventoryKeyBind = key;
                    break;
                case "OpenSkillTree":
                    _skillTreeKeyBind = key;
                    break;
                case "OpenSettings":
                    _settingsKeyBind = key;
                    break;
                case "OpenPauseMenu":
                    _pauseMenuKeyBind = key;
                    break;
                // Tab Control
                case "TabLeft":
                    _tabLeftKeyBind = key;
                    break;
                case "TabRight":
                    _tabRightKeyBind = key;
                    break;
            }
        }
    }
}
