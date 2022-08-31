using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    using Entity;
    using UI;
    
    public enum InputType
    {
        Keyboard,
        Gamepad,
    }

    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }

        [SerializeField] private KeyBindings _keyboardBinds;
        [SerializeField] private KeyBindings _gamepadBinds;

        private PlayerInputActions _inputActions;
        private readonly ISet<string> _actionNames = new HashSet<string>();
        private InputType _currentInputType = InputType.Keyboard;

        private ICharacter _player;
        private TabController _tabController;

        public KeyBindings CurrentKeyBinds => _currentInputType == InputType.Gamepad ? _gamepadBinds : _keyboardBinds;
        public InputType InputType => _currentInputType;

        public event Action<KeyBindings> OnKeyBindsChanged = delegate { };

        #region UnityEvents

        private void Awake()
        {
            // Singleton
            Instance = this;

            // Instantiate actions
            _inputActions = new();

            // Get all action names
            GetActionNames();

            // Set callbacks
            GameManager.OnPlayerChanged += OnPlayerChangedCallback;
            UnityEngine.InputSystem.InputSystem.onDeviceChange += OnDeviceChangedCallback;
        }

        private void Start()
        {
            // Get the correct key binds
            ApplyKeyBindings();

            // Enable actions
            EnableInteractionActions();
            EnableMovementActions();
            EnablePlayerUIActions();

            RegisterActions();
        }

        private void OnDisable()
        {
            DisablePlayerUIActions();
            DisableInteractionActions();
            DisableMovementActions();
            DisableTabControl();
        }

        #endregion

        #region InputDevice

        public void SetInputType(InputType type)
        {
            _currentInputType = type;
        }

        private void OnDeviceChangedCallback(InputDevice device, InputDeviceChange change)
        {
            /*switch (change)
            {
                case InputDeviceChange.Added:
                    print($"{device.displayName} Device Added!");
                    break;
                case InputDeviceChange.Removed:
                    print($"{device.displayName} Device Removed!");
                    break;
                case InputDeviceChange.Disconnected:
                    print($"{device.displayName} Device Disconnected!");
                    break;
                case InputDeviceChange.Reconnected:
                    print($"{device.displayName} Device Reconnected!");
                    break;
                case InputDeviceChange.Enabled:
                    print($"{device.displayName} Device Enabled!");
                    break;
                case InputDeviceChange.Disabled:
                    print($"{device.displayName} Device Disabled!");
                    break;
                case InputDeviceChange.UsageChanged:
                    print($"Usage Changed to {device.displayName}!");
                    break;
                case InputDeviceChange.ConfigurationChanged:
                    print($"Device Changed to {device.displayName}!");
                    break;
                case InputDeviceChange.SoftReset:
                    print($"Soft Reset to {device.displayName}!");
                    break;
                case InputDeviceChange.HardReset:
                    print($"Hard Reset to {device.displayName}!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(change), change, null);
            }*/
        }

        #endregion

        #region KeyBindModification

        private void ApplyKeyBindings()
        {
            // Keyboard Bindings
            ApplyBinds(_keyboardBinds, InputType.Keyboard);

            // Gamepad Bindings
            ApplyBinds(_gamepadBinds, InputType.Gamepad);

            // Invoke event
            OnKeyBindsChanged?.Invoke(CurrentKeyBinds);
        }

        private void ApplyBinds(in KeyBindings keyBindings, InputType inputType)
        {
            SetKeyBind(_inputActions.Interaction.Interact.name, keyBindings.InteractKeyBind, inputType, false);
            SetKeyBind(_inputActions.TabControl.TabLeft.name, keyBindings.TabLeft, inputType, false);
            SetKeyBind(_inputActions.TabControl.TabRight.name, keyBindings.TabRight, inputType, false);
            //SetKeyBind(_inputActions.PlayerUI.OpenPlayerProfile.name, keyBindings.OpenPlayerProfileKeyBind, inputType, false);
            SetKeyBind(_inputActions.PlayerUI.OpenInventory.name, keyBindings.OpenInventoryKeyBind, inputType, false);
            //SetKeyBind(_inputActions.PlayerUI.OpenSkillTree.name, keyBindings.OpenSkillTreeKeyBind, inputType, false);
            //SetKeyBind(_inputActions.PlayerUI.OpenSettings.name, keyBindings.OpenSettingsKeyBind, inputType, false);
            //SetKeyBind(_inputActions.PlayerUI.OpenPauseMenu.name, keyBindings.OpenPauseMenuKeyBind, inputType, false);
        }

        public void SetKeyBind(string actionName, KeyBind key, InputType inputType, bool refresh = true)
        {
            var bindIndex = inputType == InputType.Gamepad ? 1 : 0;

            // Update input action
            var path = InputActionRegistry.Instance.KeyCodeToBindingString(key);
            _inputActions.asset[actionName]
                .ChangeBinding(_inputActions.asset[actionName].bindings[bindIndex])
                .WithPath(path);

            // Update key binds
            if (refresh)
                RefreshKeyBind(actionName, inputType);
        }

        private void RefreshKeyBind(string actionName, InputType inputType)
        {
            var bindIndex = inputType == InputType.Gamepad ? 1 : 0;

            // Assign key
            CurrentKeyBinds[actionName] =
                InputActionRegistry.Instance.BindingStringToKeyCode(_inputActions.asset[actionName].bindings[bindIndex]
                    .path);

            // Invoke event
            OnKeyBindsChanged?.Invoke(CurrentKeyBinds);
        }

        public KeyBind ResetKeyBind(string actionName, InputType inputType)
        {
            var bindIndex = inputType == InputType.Gamepad ? 1 : 0;

            // Create default actions
            var defaultBinding = new PlayerInputActions().asset[actionName].bindings[bindIndex];

            // Reset action key bind
            _inputActions.asset[actionName]
                .ChangeBinding(_inputActions.asset[actionName].bindings[bindIndex])
                .WithPath(defaultBinding.path);

            // Refresh key bind
            RefreshKeyBind(actionName, inputType);

            // Return default action
            return InputActionRegistry.Instance.BindingStringToKeyCode(defaultBinding.path);
        }

        #endregion

        #region ActionHandling

        private void RegisterActions()
        {
            _inputActions.PlayerUI.OpenInventory.performed += OpenInventoryAction;
            _inputActions.PlayerUI.OpenPauseMenu.performed += OpenPauseMenuAction;
            _inputActions.TabControl.TabLeft.performed += TabLeftAction;
            _inputActions.TabControl.TabRight.performed += TabRightAction;
        }

        private void OnPlayerChangedCallback(ICharacter player)
        {
            // Assign new player
            _player = player;
        }

        private void OpenInventoryAction(InputAction.CallbackContext context)
        {
            PlayerMenuController.Instance.ToggleInventory();
        }
        
        private void OpenPauseMenuAction(InputAction.CallbackContext context)
        {
            SettingsMenuController.Instance.Toggle();
        }

        private void TabLeftAction(InputAction.CallbackContext context)
        {
            _tabController.TabLeft();
        }
        
        private void TabRightAction(InputAction.CallbackContext context)
        {
            _tabController.TabRight();
        }

        public void SetCallback(string actionName, Action<InputAction.CallbackContext> callback)
        {
            _inputActions.asset[actionName].performed += callback;
        }

        public void RemoveCallback(string actionName, Action<InputAction.CallbackContext> callback)
        {
            _inputActions.asset[actionName].performed -= callback;
        }

        public void EnableInteractionActions()
        {
            _inputActions.Interaction.Enable();
        }

        public void DisableInteractionActions()
        {
            _inputActions.Interaction.Disable();
        }

        public void EnableMovementActions()
        {
            _inputActions.Movement.Enable();
        }

        public void DisableMovementActions()
        {
            _inputActions.Movement.Disable();
        }

        public void EnableGeneralUIActions()
        {
            _inputActions.GeneralUI.Enable();
        }

        public void DisableGeneralUIActions()
        {
            _inputActions.GeneralUI.Disable();
        }

        private void EnablePlayerUIActions()
        {
            _inputActions.PlayerUI.Enable();
        }

        private void DisablePlayerUIActions()
        {
            _inputActions.PlayerUI.Disable();
        }

        private void EnableTabControl()
        {
            _inputActions.TabControl.Enable();
        }

        private void DisableTabControl()
        {
            _inputActions.TabControl.Disable();
        }

        #endregion

        #region Inputs

        public InputAction GetPlayerInputAction(string actionName)
        {
            return _inputActions.FindAction(actionName);
        }
        
        public void SetCurrentTabController(TabController tabController)
        {
            _tabController = tabController;
            EnableTabControl();
        }
        
        public void RemoveCurrentTabController()
        {
            DisableTabControl();
            _tabController = null;
        }

        #endregion

        #region Misc

        private void GetActionNames()
        {
            var inputBindings = _inputActions.asset.bindings;

            // Add action names
            foreach (var inputBinding in inputBindings)
            {
                _actionNames.Add(inputBinding.name);
            }
        }

        #endregion
    }
}
