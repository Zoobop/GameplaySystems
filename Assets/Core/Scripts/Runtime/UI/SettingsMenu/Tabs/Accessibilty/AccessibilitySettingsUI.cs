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
        }

        private void UnbindUIElements()
        {
            LocalizationSystem.OnLanguageChanged -= OnLanguageChangedCallback;
            
            _languageDropdown.RemoveListener(SetLanguage);
        }

        private void SetupSettings()
        {
            // UI Setup
            _languageDropdown.SetOptions(_languageNames);
            _languageDropdown.Apply();

            var settings = SettingsController.Instance;
            
            // Set default values
            _languageDropdown.SetValue((int)settings.CurrentLanguage);
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