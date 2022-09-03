using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    using Settings;
    using InputSystem;
    using LocalizationSystem;

    public class ControlsSettingsUI : BaseSettingsUI
    {
        [Header("General")]
        [SerializeField] private EnhancedDropdown _inputTypeDropdown;

        [Header("Mouse and Keyboard")]
        [SerializeField] private EnhancedSlider _mouseHorizontalSensitivitySlider;
        [SerializeField] private EnhancedSlider _mouseVerticalSensitivitySlider;
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
            _mouseHorizontalSensitivitySlider.AddListener(SetMouseHorizontalSensitivity);
            _mouseVerticalSensitivitySlider.AddListener(SetMouseVerticalSensitivity);
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
            _mouseHorizontalSensitivitySlider.RemoveListener(SetMouseHorizontalSensitivity);
            _mouseVerticalSensitivitySlider.RemoveListener(SetMouseVerticalSensitivity);
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
            _mouseHorizontalSensitivitySlider.SetBounds(0, 1);
            _mouseVerticalSensitivitySlider.SetBounds(0, 1);
            _gamepadHorizontalSensitivitySlider.SetBounds(0, 1);
            _gamepadVerticalSensitivitySlider.SetBounds(0, 1);
            
            var settings = SettingsController.Instance;
            
            // Set default values
            _inputTypeDropdown.SetValue((int)settings.InputType);
            _mouseHorizontalSensitivitySlider.SetValue(settings.MouseSensitivityHorizontal);
            _mouseVerticalSensitivitySlider.SetValue(settings.MouseSensitivityVertical);
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
            InputController.Instance.SetInputType((InputType)index);
        }

        private void SetMouseHorizontalSensitivity(float value)
        {
            
        }
        
        private void SetMouseVerticalSensitivity(float value)
        {
            
        }
        
        private void SetMouseHorizontalInvert(bool value)
        {
            
        }
        
        private void SetMouseVerticalInvert(bool value)
        {
            
        }
        
        private void SetGamepadHorizontalSensitivity(float value)
        {
            
        }
        
        private void SetGamepadVerticalSensitivity(float value)
        {
            
        }
        
        private void SetGamepadHorizontalInvert(bool value)
        {
            
        }
        
        private void SetGamepadVerticalInvert(bool value)
        {
            
        }
        
        #endregion
    }
}