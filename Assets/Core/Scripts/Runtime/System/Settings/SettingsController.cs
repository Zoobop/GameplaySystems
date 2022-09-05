using System;
using UnityEngine;

namespace Settings
{
    using LocalizationSystem;
    using InputSystem;
    
    public class SettingsController : MonoBehaviour
    {
        public static SettingsController Instance { get; private set; }

        /* GENERAL */
        [field: Header("General Settings")]
        [field: SerializeField] public LocalizationSystem.Language CurrentLanguage { get; set; } = LocalizationSystem.Language.English;
        
        /* GRAPHICS */
        [field: Header("Graphics Settings")]
        [field: SerializeField] public bool DisplayFPS { get; set; } = true;
        [field: SerializeField] public bool DisplayTime { get; set; } = true;

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
        
        #region UnityEvents

        private void Awake()
        {
            Instance = this;
            LocalizationSystem.SetLanguage(CurrentLanguage);
        }

        private void Start()
        {
            InputController.SetInputType(InputType);
        }

        #endregion

        #region GeneralSettings

        

        #endregion

        #region GraphicsSettings

        

        #endregion

        #region AudioSettings

        

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

        #endregion

        #region AccessibilitySettings

        

        #endregion
    }
}
