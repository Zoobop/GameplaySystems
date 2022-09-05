using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RebindOperation = UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation;

namespace InputSystem
{
    using UI;

    public enum BindType
    {
        SingleBind,
        TwoWayComposite,
        FourWayComposite,
    }
    
    public enum InputType
    {
        Keyboard,
        Gamepad,
    }

    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }

        /* Input Scheme */
        [SerializeField] private InputType _currentInputType = InputType.Keyboard;
        
        /* Mouse/Keyboard */
        [SerializeField] private KeyBindings _keyboardBinds;
        
        /* Gamepad */
        [SerializeField] private KeyBindings _gamepadBinds;

        private static PlayerInputActions _defaultActions;
        private static PlayerInputActions _inputActions;
        private static TabController _tabController;
        
        /* Rebinding */
        private static readonly WaitForSeconds TickDelay = new WaitForSeconds(0.01f);
        private static readonly string[] CancelButtons = {"<Mouse>/rightButton", "<Gamepad>/buttonEast"};

        public static KeyBindings CurrentKeyBinds => GetCurrentKeyBindings();
        public static InputType InputType => Instance._currentInputType;

        public static event Action<KeyBindings> OnKeyBindsChanged = delegate { };
        public static event Action<InputType> OnInputChanged = delegate { };

        #region UnityEvents

        private void Awake()
        {
            // Singleton
            Instance = this;
            
            // 

            // Instantiate actions
            _defaultActions = new PlayerInputActions();
            _inputActions = new PlayerInputActions();
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
            DisableTabControlActions();
        }

        #endregion

        #region InputDevice

        public static void SetInputType(InputType type)
        {
            Instance._currentInputType = type;
            
            // Invoke event
            OnKeyBindsChanged?.Invoke(CurrentKeyBinds);
            OnInputChanged?.Invoke(type);
        }

        #endregion

        #region Setup

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
            if (inputType == InputType.Keyboard)
            {
                SetKeyComposite(_inputActions.Movement.Move.name, "MoveForward", keyBindings.MoveForwardKeyBind, inputType, 1, false);
                SetKeyComposite(_inputActions.Movement.Move.name, "MoveBack", keyBindings.MoveBackKeyBind, inputType, 2, false);
                SetKeyComposite(_inputActions.Movement.Move.name, "MoveLeft", keyBindings.MoveLeftKeyBind, inputType, 3, false);
                SetKeyComposite(_inputActions.Movement.Move.name, "MoveRight", keyBindings.MoveRightKeyBind, inputType, 4, false);
            }
            else
            {
                SetKeyComposite(_inputActions.Movement.Move.name, "Movement", keyBindings.MovementKeyBind, inputType, 4, false);
            }
            
            SetKeyBind(_inputActions.Interaction.Interact.name, keyBindings.InteractKeyBind, inputType, false);
            SetKeyBind(_inputActions.TabControl.TabLeft.name, keyBindings.TabLeftKeyBind, inputType, false);
            SetKeyBind(_inputActions.TabControl.TabRight.name, keyBindings.TabRightKeyBind, inputType, false);
            SetKeyBind(_inputActions.PlayerUI.OpenInventory.name, keyBindings.OpenInventoryKeyBind, inputType, false);
            SetKeyBind(_inputActions.PlayerUI.OpenPauseMenu.name, keyBindings.OpenPauseMenuKeyBind, inputType, false);
        }

        private static void SetKeyBind(string actionName, KeyBind key, InputType inputType, bool refresh = true)
        {
            var bindIndex = inputType == InputType.Gamepad ? 1 : 0;

            // Update input action
            var path = InputActionRegistry.KeyCodeToBindingString(key);
            _inputActions.asset[actionName]
                .ChangeBinding(_inputActions.asset[actionName].bindings[bindIndex])
                .WithPath(path);

            // Update key binds
            if (refresh)
                RefreshKeyBind(actionName, bindIndex);
        }

        private static void SetKeyComposite(string actionName, string keyBindName, KeyBind key, InputType inputType, int relativeBindIndex,
            bool refresh = true)
        {
            var inputOffset = inputType == InputType.Gamepad ? 1 : 0;
            var bindIndex = inputOffset + relativeBindIndex;

            // Update input action
            var path = InputActionRegistry.KeyCodeToBindingString(key);
            _inputActions.asset[actionName]
                .ChangeBinding(_inputActions.asset[actionName].bindings[bindIndex])
                .WithPath(path);

            // Update key binds
            if (refresh)
                RefreshKeyBind(actionName, keyBindName, bindIndex);
        }

        private static KeyBindings GetCurrentKeyBindings()
        {
            return Instance._currentInputType == InputType.Gamepad ? Instance._gamepadBinds : Instance._keyboardBinds;
        }

        #endregion
        
        #region KeyBindModification

        private static IEnumerator AwaitRebindKeyBind(string actionName, string keyBindName, int bindIndex, Action onRebindEnded)
        {
            // Disable actions for modification
            DisableTabControlActions();
            DisablePlayerUIActions();
            
            // Setup rebind operation
            using var rebindingOperation = _inputActions.asset[actionName].PerformInteractiveRebinding(bindIndex);

            void ApplyBinding(RebindOperation rebindingOp, string path)
            {
                rebindingOp.action.ChangeBinding(bindIndex).WithPath(path);
            }

            rebindingOperation.OnApplyBinding(ApplyBinding);
            
            // Input Cancel
            rebindingOperation.WithCancelingThrough(CancelButtons[0]);
            rebindingOperation.WithCancelingThrough(CancelButtons[1]);

            // Start and Wait for rebind operation to stop
            rebindingOperation.Start();
            while (!rebindingOperation.completed && !rebindingOperation.canceled)
            {
                yield return TickDelay;
            }

            // Re-enable actions
            EnablePlayerUIActions();
            EnableTabControlAction();

            // Invoke action
            onRebindEnded?.Invoke();

            // Refresh the key bind
            var name = rebindingOperation.action.name;
            RefreshKeyBind(name, keyBindName, bindIndex);
        }

        private static void RefreshKeyBind(string actionName, string keyBindName, int bindIndex)
        {
            // Assign key
            var path = _inputActions.asset[actionName].bindings[bindIndex].path;
            print(path);
            CurrentKeyBinds[keyBindName] = InputActionRegistry.BindingStringToKeyCode(path);
            print(CurrentKeyBinds[keyBindName]);

            // Invoke event
            OnKeyBindsChanged?.Invoke(CurrentKeyBinds);
        }
        
        private static void RefreshKeyBind(string actionName, int bindIndex)
        {
            // Assign key
            var path = _inputActions.asset[actionName].bindings[bindIndex].path;
            print(path);
            CurrentKeyBinds[actionName] = InputActionRegistry.BindingStringToKeyCode(path);
            print(CurrentKeyBinds[actionName]);

            // Invoke event
            OnKeyBindsChanged?.Invoke(CurrentKeyBinds);
        }

        public static void RebindKeyBind(string actionName, string keyBindName, int bindIndex, Action onRebindEnd = null)
        {
            Instance.StartCoroutine(AwaitRebindKeyBind(actionName, keyBindName, bindIndex, onRebindEnd));
        }

        public static void ResetKeyBind(string actionName, string keyBindName, int bindIndex)
        {
            // Create default actions
            var defaultBinding = _defaultActions.asset[actionName].bindings[bindIndex];

            // Reset action key bind
            _inputActions.asset[actionName]
                .ChangeBinding(_inputActions.asset[actionName].bindings[bindIndex])
                .WithPath(defaultBinding.path);

            // Refresh key bind
            RefreshKeyBind(actionName, keyBindName, bindIndex);
        }

        #endregion

        #region ActionHandling

        private static void RegisterActions()
        {
            _inputActions.PlayerUI.OpenInventory.performed += OpenInventoryAction;
            _inputActions.PlayerUI.OpenPauseMenu.performed += OpenPauseMenuAction;
            _inputActions.TabControl.TabLeft.performed += TabLeftAction;
            _inputActions.TabControl.TabRight.performed += TabRightAction;
        }

        private static void OpenInventoryAction(InputAction.CallbackContext context) => PlayerMenuController.Instance.ToggleInventory();
        private static void OpenPauseMenuAction(InputAction.CallbackContext context) => SettingsMenuController.Instance.Toggle();
        private static void TabLeftAction(InputAction.CallbackContext context) => _tabController.TabLeft();
        private static void TabRightAction(InputAction.CallbackContext context) => _tabController.TabRight();
        public static void EnableInteractionActions() => _inputActions.Interaction.Enable();
        public static void DisableInteractionActions() => _inputActions.Interaction.Disable();
        public static void EnableMovementActions() => _inputActions.Movement.Enable();
        public static void DisableMovementActions() => _inputActions.Movement.Disable();
        public static void EnableGeneralUIActions() => _inputActions.GeneralUI.Enable();
        public static void DisableGeneralUIActions() => _inputActions.GeneralUI.Disable();
        private static void EnablePlayerUIActions() => _inputActions.PlayerUI.Enable();
        private static void DisablePlayerUIActions() => _inputActions.PlayerUI.Disable();
        private static void EnableTabControlAction() => _inputActions.TabControl.Enable();
        private static void DisableTabControlActions() => _inputActions.TabControl.Disable();

        #endregion

        #region Inputs

        public static InputAction GetPlayerInputAction(string actionName) => _inputActions.FindAction(actionName);
        public static void SetCallback(string actionName, Action<InputAction.CallbackContext> callback) => _inputActions.asset[actionName].performed += callback;
        public static void RemoveCallback(string actionName, Action<InputAction.CallbackContext> callback) => _inputActions.asset[actionName].performed -= callback;
        
        public static void SetCurrentTabController(TabController tabController)
        {
            _tabController = tabController;
            EnableTabControlAction();
        }
        
        public static void RemoveCurrentTabController()
        {
            DisableTabControlActions();
            _tabController = null;
        }

        #endregion
    }
}
