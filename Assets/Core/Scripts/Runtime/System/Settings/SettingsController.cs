using System;
using UnityEngine;

namespace Settings
{
    using LocalizationSystem;
    using InputSystem;

    public enum ScreenView
    {
        FullscreenWindowed,
        Fullscreen,
        Windowed
    }
    
    public class SettingsController : MonoBehaviour
    {
        public static SettingsController Instance { get; private set; }

        /* GENERAL */
        [field: Header("General Settings")]
        [field: SerializeField] public LocalizationSystem.Language CurrentLanguage { get; set; } = LocalizationSystem.Language.English;
        
        /* GRAPHICS */
        [field: Header("Graphics Settings")]
        [field: SerializeField] public int ResolutionIndex { get; set; } = 0;
        [field: SerializeField] public ScreenView WindowMode { get; set; } = ScreenView.FullscreenWindowed;
        [field: SerializeField, Range(0f, 1f)] public float Gamma { get; set; } = 0.5f;

        /* AUDIO */
        [field: Header("Audio Settings")]
        [field: SerializeField, Range(0f, 1f)] public float MasterVolume { get; set; } = 1f; // 1 is max, 0 is min
        [field: SerializeField, Range(0f, 1f)] public float MainMenuVolume { get; set; } = 1f;
        [field: SerializeField, Range(0f, 1f)] public float BGMVolume { get; set; } = 1f;
        [field: SerializeField, Range(0f, 1f)] public float SoundEffectsVolume { get; set; } = 1f;
        
        /* CONTROLS */
        [field: Header("Controls Settings")]
        [field: SerializeField] public InputType InputType { get; set; } = InputType.Keyboard;
        [field: SerializeField, Range(0f, 1f)] public float MouseSensitivity { get; set; } = 0.5f; // 1 will be 100, 0 will be 0
        [field: SerializeField, Range(0f, 2f)] public float MouseScaleHorizontal { get; set; } = 1.0f; // 1 will be 200
        [field: SerializeField, Range(0f, 2f)] public float MouseScaleVertical { get; set; } = 1.0f;
        [field: SerializeField] public bool MouseInvertHorizontal { get; set; } = false;
        [field: SerializeField] public bool MouseInvertVertical { get; set; } = false;
        [field: SerializeField, Range(0f, 1f)] public float GamepadSensitivityHorizontal { get; set; } = 0.5f;
        [field: SerializeField, Range(0f, 1f)] public float GamepadSensitivityVertical { get; set; } = 0.5f;
        [field: SerializeField] public bool GamepadInvertHorizontal { get; set; } = false;
        [field: SerializeField] public bool GamepadInvertVertical { get; set; } = false;
        
        /* ACCESSIBILITY */
        [field: Header("Accessibility Settings")]
        [field: SerializeField] public bool DisplayFPS { get; set; } = true;
        [field: SerializeField] public bool DisplayTime { get; set; } = true;
        
        #region UnityEvents

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ApplyGeneralSettings();
            ApplyGraphicsSettings();
            ApplyAudioSettings();
            ApplyControlsSettings();
            ApplyAccessibilitySettings();
        }

        #endregion

        #region GeneralSettings

        private static void ApplyGeneralSettings()
        {
            
        }

        #endregion

        #region GraphicsSettings

        public static void SetResolution(int index)
        {
            Instance.ResolutionIndex = index;
            var resolution = Screen.resolutions[Screen.resolutions.Length - 1 - index];

            var fullscreen = Instance.WindowMode is ScreenView.Fullscreen or ScreenView.FullscreenWindowed;
            Screen.SetResolution(resolution.width, resolution.height, fullscreen);
        }
        
        public static void SetWindowMode(int index)
        {
            Instance.WindowMode = (ScreenView) index;
            var screenView = index switch
            {
                0 => FullScreenMode.FullScreenWindow,
                #if PLATFORM_STANDALONE_WIN
                1 => FullScreenMode.MaximizedWindow,
                #elif PLATFORM_STANDALONE_OSX
                1 => FullScreenMode.ExclusiveFullScreen,
                #endif
                2 => FullScreenMode.Windowed,
                _ => throw new ArgumentOutOfRangeException()
            };
            Screen.fullScreenMode = screenView;
        }

        public static void SetGamma(float gamma)
        {
            Instance.Gamma = gamma;
            Screen.brightness = gamma;
        }
        
        private static void ApplyGraphicsSettings()
        {
            SetResolution(Instance.ResolutionIndex);
            SetWindowMode((int)Instance.WindowMode);
            SetGamma(Instance.Gamma);
        }

        #endregion

        #region AudioSettings

        public static void SetMasterVolume(float volume)
        {
            Instance.MasterVolume = volume;
            AudioController.SetMasterVolume(volume);
        }
        
        public static void SetMainMenuVolume(float volume)
        {
            Instance.MainMenuVolume = volume;
            AudioController.SetMainMenuVolume(volume);
        }
        
        public static void SetBGMVolume(float volume)
        {
            Instance.BGMVolume = volume;
            AudioController.SetBGMVolume(volume);
        }
        
        public static void SetSfxVolume(float volume)
        {
            Instance.SoundEffectsVolume = volume;
            AudioController.SetSfxVolume(volume);
        }

        private static void ApplyAudioSettings()
        {
            AudioController.SetMasterVolume(Instance.MasterVolume);
            AudioController.SetMainMenuVolume(Instance.MainMenuVolume);
            AudioController.SetBGMVolume(Instance.BGMVolume);
            AudioController.SetSfxVolume(Instance.SoundEffectsVolume);
        }
        
        #endregion

        #region ControlsSettings

        public static void SetInputType(int index)
        {
            Instance.InputType = (InputType)index;
            InputController.SetInputType(Instance.InputType);
        }

        public static void SetMouseSensitivity(float value)
        {
            Instance.MouseSensitivity = value;
        }
        
        public static void SetMouseScaleHorizontal(float value)
        {
            Instance.MouseScaleHorizontal = value;
        }
        
        public static void SetMouseScaleVertical(float value)
        {
            Instance.MouseScaleVertical = value;
        }
        
        public static void SetMouseInvertHorizontal(bool value)
        {
            Instance.MouseInvertHorizontal = value;
        }
        
        public static void SetMouseInvertVertical(bool value)
        {
            Instance.MouseInvertVertical = value;
        }
        
        public static void SetGamepadSensitivityHorizontal(float value)
        {
            Instance.GamepadSensitivityHorizontal = value;
        }
        
        public static void SetGamepadSensitivityVertical(float value)
        {
            Instance.GamepadSensitivityVertical = value;
        }
        
        public static void SetGamepadInvertHorizontal(bool value)
        {
            Instance.GamepadInvertHorizontal = value;
        }
        
        public static void SetGamepadInvertVertical(bool value)
        {
            Instance.GamepadInvertVertical = value;
        }

        private static void ApplyControlsSettings()
        {
            InputController.SetInputType(Instance.InputType);
        }
        
        #endregion

        #region AccessibilitySettings

        public static void SetLanguage(int index)
        {
            Instance.CurrentLanguage = (LocalizationSystem.Language) index;
            LocalizationSystem.SetLanguage(Instance.CurrentLanguage);
        }

        public static void SetDisplayFps(bool state)
        {
            Instance.DisplayFPS = state;
        }
        
        public static void SetDisplayTime(bool state)
        {
            Instance.DisplayTime = state;
        }
        
        private static void ApplyAccessibilitySettings()
        {
            LocalizationSystem.SetLanguage(Instance.CurrentLanguage);
        }

        #endregion
    }
}
