using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    using Settings;
    
    public class AudioSettingsUI : BaseSettingsUI
    {
        [Header("Audio")]
        [SerializeField] private EnhancedSlider _masterSlider;
        [SerializeField] private EnhancedSlider _mainMenuSlider;
        [SerializeField] private EnhancedSlider _bgmSlider;
        [SerializeField] private EnhancedSlider _sfxSlider;
        
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
            _masterSlider.AddListener(SetMaster);
            _mainMenuSlider.AddListener(SetMainMenu);
            _bgmSlider.AddListener(SetBGM);
            _sfxSlider.AddListener(SetSfx);
        }

        private void UnbindUIElements()
        {
            _masterSlider.RemoveListener(SetMaster);
            _mainMenuSlider.RemoveListener(SetMainMenu);
            _bgmSlider.RemoveListener(SetBGM);
            _sfxSlider.RemoveListener(SetSfx);
        }

        private void SetupSettings()
        {
            // UI Setup
            _masterSlider.SetBounds(0, 1);
            _mainMenuSlider.SetBounds(0, 1);
            _bgmSlider.SetBounds(0, 1);
            _sfxSlider.SetBounds(0, 1);

            var settings = SettingsController.Instance;
            
            // Set default values
            _masterSlider.SetValue(settings.MasterVolume);
            _mainMenuSlider.SetValue(settings.MainMenuVolume);
            _bgmSlider.SetValue(settings.BGMVolume);
            _sfxSlider.SetValue(settings.SoundEffectsVolume);
        }

        #endregion

        #region ApplySettings

        private void SetMaster(float volume)
        {
            SettingsController.SetMasterVolume(volume);
        }

        private void SetMainMenu(float volume)
        {
            SettingsController.SetMainMenuVolume(volume);
        }

        private void SetBGM(float volume)
        {
            SettingsController.SetBGMVolume(volume);
        }
        
        private void SetSfx(float volume)
        {
            SettingsController.SetSfxVolume(volume);
        }

        #endregion
    }
}