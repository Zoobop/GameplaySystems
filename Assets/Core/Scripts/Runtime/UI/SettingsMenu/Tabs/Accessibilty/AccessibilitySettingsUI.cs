using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    using Settings;
    
    public class AccessibilitySettingsUI : BaseSettingsUI
    {
        [Header("Language")]
        [SerializeField] private EnhancedDropdown _languageDropdown;
        
        [Header("Info")]
        [SerializeField] private EnhancedToggle _displayFpsToggle;
        [SerializeField] private EnhancedToggle _displayTimeToggle;

        private readonly List<string> _languageNames = new List<string> {"English", "日本語"};
        
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
            LocalizationSystem.OnLanguageChanged += OnLanguageChangedCallback;
            
            _languageDropdown.AddListener(SetLanguage);
            _displayFpsToggle.AddListener(SetDisplayFps);
            _displayTimeToggle.AddListener(SetDisplayTime);
        }

        private void UnbindUIElements()
        {
            LocalizationSystem.OnLanguageChanged -= OnLanguageChangedCallback;
            
            _languageDropdown.RemoveListener(SetLanguage);
            _displayFpsToggle.RemoveListener(SetDisplayFps);
            _displayTimeToggle.RemoveListener(SetDisplayTime);
        }

        private void SetupSettings()
        {
            // UI Setup
            _languageDropdown.SetOptions(_languageNames);
            _languageDropdown.Apply();

            var settings = SettingsController.Instance;
            
            // Set default values
            _languageDropdown.SetValue((int)settings.CurrentLanguage);
            _displayFpsToggle.SetValue(settings.DisplayFPS);
            _displayTimeToggle.SetValue(settings.DisplayTime);
        }

        private void OnLanguageChangedCallback(LocalizationSystem.Language language)
        {
            _languageDropdown.SetOptions(_languageNames);
            _languageDropdown.SetValue((int)language);
        }

        #endregion

        #region ApplySettings

        private void SetLanguage(int index)
        {
            SettingsController.SetLanguage(index);
        }

        private void SetDisplayFps(bool state)
        {
            SettingsController.SetDisplayFps(state);
        }

        private void SetDisplayTime(bool state)
        {
            SettingsController.SetDisplayTime(state);
        }

        #endregion
    }
}