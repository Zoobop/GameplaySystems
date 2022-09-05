using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    using Settings;
    using LocalizationSystem;

    public class ControlsSettingsUI : BaseSettingsUI
    {
        [Header("References")] 
        [SerializeField] private KeyBindModificationWindow _keyBindModificationWindow;
        
        [Header("General")]
        [SerializeField] private EnhancedDropdown _inputTypeDropdown;

        [Header("Mouse and Keyboard")]
        [SerializeField] private EnhancedSlider _mouseSensitivitySlider;
        [SerializeField] private EnhancedSlider _mouseHorizontalScaleSlider;
        [SerializeField] private EnhancedSlider _mouseVerticalScaleSlider;
        [SerializeField] private EnhancedToggle _mouseInvertHorizontalToggle;
        [SerializeField] private EnhancedToggle _mouseInvertVerticalToggle;
        
        [Header("Gamepad")]
        [SerializeField] private EnhancedSlider _gamepadHorizontalSensitivitySlider;
        [SerializeField] private EnhancedSlider _gamepadVerticalSensitivitySlider;
        [SerializeField] private EnhancedToggle _gamepadInvertHorizontalToggle;
        [SerializeField] private EnhancedToggle _gamepadInvertVerticalToggle;

        #region UnityEvents

        private void Awake()
        {
            BindUIElements();
        }

        private void Start()
        {
            SetupSettings();
        }

        private void OnDestroy()
        {
            UnbindUIElements();
        }

        #endregion

        #region Setup

        private void BindUIElements()
        {
            _inputTypeDropdown.AddListener(SetInputType);
            _mouseSensitivitySlider.AddListener(SetMouseSensitivity);
            _mouseHorizontalScaleSlider.AddListener(SetMouseHorizontalScale);
            _mouseVerticalScaleSlider.AddListener(SetMouseVerticalScale);
            _mouseInvertHorizontalToggle.AddListener(SetMouseHorizontalInvert);
            _mouseInvertVerticalToggle.AddListener(SetMouseVerticalInvert);
            _gamepadHorizontalSensitivitySlider.AddListener(SetGamepadHorizontalSensitivity);
            _gamepadVerticalSensitivitySlider.AddListener(SetGamepadVerticalSensitivity);
            _gamepadInvertHorizontalToggle.AddListener(SetGamepadHorizontalInvert);
            _gamepadInvertVerticalToggle.AddListener(SetGamepadVerticalInvert);
        }

        private void UnbindUIElements()
        {
            _inputTypeDropdown.RemoveListener(SetInputType);
            _mouseSensitivitySlider.RemoveListener(SetMouseSensitivity);
            _mouseHorizontalScaleSlider.RemoveListener(SetMouseHorizontalScale);
            _mouseVerticalScaleSlider.RemoveListener(SetMouseVerticalScale);
            _mouseInvertHorizontalToggle.RemoveListener(SetMouseHorizontalInvert);
            _mouseInvertVerticalToggle.RemoveListener(SetMouseVerticalInvert);
            _gamepadHorizontalSensitivitySlider.RemoveListener(SetGamepadHorizontalSensitivity);
            _gamepadVerticalSensitivitySlider.RemoveListener(SetGamepadVerticalSensitivity);
            _gamepadInvertHorizontalToggle.RemoveListener(SetGamepadHorizontalInvert);
            _gamepadInvertVerticalToggle.RemoveListener(SetGamepadVerticalInvert);
        }
        
        private void SetupSettings()
        {
            var names = new List<string> { new LocalizedString("ui_settings_controls_mnk"), new LocalizedString("ui_settings_controls_gamepad") };

            // UI Setup
            _inputTypeDropdown.SetOptions(names);
            _inputTypeDropdown.Apply();
            _mouseSensitivitySlider.SetBounds(0, 1);
            _mouseHorizontalScaleSlider.SetBounds(0, 2);
            _mouseVerticalScaleSlider.SetBounds(0, 2);
            _gamepadHorizontalSensitivitySlider.SetBounds(0, 1);
            _gamepadVerticalSensitivitySlider.SetBounds(0, 1);
            
            var settings = SettingsController.Instance;
            
            // Set default values
            _inputTypeDropdown.SetValue((int)settings.InputType);
            _mouseSensitivitySlider.SetValue(settings.MouseSensitivity);
            _mouseHorizontalScaleSlider.SetValue(settings.MouseScaleHorizontal);
            _mouseVerticalScaleSlider.SetValue(settings.MouseScaleVertical);
            _mouseInvertHorizontalToggle.SetValue(settings.MouseInvertHorizontal);
            _mouseInvertVerticalToggle.SetValue(settings.MouseInvertVertical);
            _gamepadHorizontalSensitivitySlider.SetValue(settings.GamepadSensitivityHorizontal);
            _gamepadVerticalSensitivitySlider.SetValue(settings.GamepadSensitivityVertical);
            _gamepadInvertHorizontalToggle.SetValue(settings.GamepadInvertHorizontal);
            _gamepadInvertVerticalToggle.SetValue(settings.GamepadInvertVertical);
        }

        #endregion
        
        #region ApplySettings

        private void SetInputType(int index)
        {
            SettingsController.SetInputType(index);
        }

        private void SetMouseSensitivity(float value)
        {
            SettingsController.SetMouseSensitivity(value);
        }
        
        private void SetMouseHorizontalScale(float value)
        {
            SettingsController.SetMouseScaleHorizontal(value);
        }
        
        private void SetMouseVerticalScale(float value)
        {
            SettingsController.SetMouseScaleVertical(value);
        }
        
        private void SetMouseHorizontalInvert(bool value)
        {
            SettingsController.SetMouseInvertHorizontal(value);
        }
        
        private void SetMouseVerticalInvert(bool value)
        {
            SettingsController.SetMouseInvertVertical(value);
        }
        
        private void SetGamepadHorizontalSensitivity(float value)
        {
            SettingsController.SetGamepadSensitivityHorizontal(value);
        }
        
        private void SetGamepadVerticalSensitivity(float value)
        {
            SettingsController.SetGamepadSensitivityVertical(value);
        }
        
        private void SetGamepadHorizontalInvert(bool value)
        {
            SettingsController.SetGamepadInvertHorizontal(value);
        }
        
        private void SetGamepadVerticalInvert(bool value)
        {
            SettingsController.SetGamepadInvertVertical(value);
        }
        
        #endregion

        public void OpenKeyBindWindow(KeyBindUI keyBindUI)
        {
            _keyBindModificationWindow.SetKeyBindButton(keyBindUI);
            _keyBindModificationWindow.Enable();
        }

        public void CloseKeyBindWindow()
        {
            _keyBindModificationWindow.Disable();
        }
    }
}