using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    using Settings;
    
    public class GraphicsSettingsUI : BaseSettingsUI
    {
        [Header("References")]
        [SerializeField] private EnhancedDropdown _resolutionDropdown;
        [SerializeField] private EnhancedDropdown _screenViewDropdown;
        [SerializeField] private EnhancedSlider _gammaSlider;

        private static readonly LocalizedString Windowed = new LocalizedString("ui_settings_graphics_screen_windowed");
        private static readonly LocalizedString FullscreenWindowed = new LocalizedString("ui_settings_graphics_screen_fullscreen_windowed");
        private static readonly LocalizedString Fullscreen = new LocalizedString("ui_settings_graphics_screen_fullscreen");

        private static readonly List<string> WindowOptions = new(3);

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
            
            _resolutionDropdown.AddListener(SetResolution);
            _screenViewDropdown.AddListener(SetScreenView);
            _gammaSlider.AddListener(SetGamma);
        }

        private void UnbindUIElements()
        {
            LocalizationSystem.OnLanguageChanged -= OnLanguageChangedCallback;
            
            _resolutionDropdown.RemoveListener(SetResolution);
            _screenViewDropdown.RemoveListener(SetScreenView);
            _gammaSlider.RemoveListener(SetGamma);
        }

        private void SetupSettings()
        {
            var resolutions = new List<string>(Screen.resolutions.Length);
            for (var index = Screen.resolutions.Length - 1; index >= 0; index--)
            {
                var resolution = Screen.resolutions[index];
                resolutions.Add($"{resolution.width}x{resolution.height}");
            }
            
            WindowOptions.Add(Fullscreen);
            WindowOptions.Add(FullscreenWindowed);
            WindowOptions.Add(Windowed);

            // UI Setup
            _resolutionDropdown.SetOptions(resolutions);
            _resolutionDropdown.Apply();
            _screenViewDropdown.SetOptions(WindowOptions);
            _screenViewDropdown.Apply();
            _gammaSlider.SetBounds(0, 1);
            
            var settings = SettingsController.Instance;
            
            // Set default values
            _resolutionDropdown.SetValue(settings.ResolutionIndex);
            _screenViewDropdown.SetValue((int)settings.WindowMode);
            _gammaSlider.SetValue(settings.Gamma);
        }

        #endregion

        private void OnLanguageChangedCallback(LocalizationSystem.Language language)
        {
            _screenViewDropdown.SetOptions(WindowOptions);
        }
        
        #region ApplySettings

        private void SetResolution(int index)
        {
            SettingsController.SetResolution(index);
        }

        private void SetScreenView(int index)
        {
            SettingsController.SetWindowMode(index);
        }

        private void SetGamma(float gamma)
        {
            SettingsController.SetGamma(gamma);
        }

        #endregion
    }
}