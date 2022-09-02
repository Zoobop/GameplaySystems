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
        [field: SerializeField, Range(0f, 1f)] public float MouseSensitivityHorizontal { get; set; } = 0.5f; // 1 will be 100, 0 will be 0
        [field: SerializeField, Range(0f, 1f)] public float MouseSensitivityVertical { get; set; } = 0.5f;
        [field: SerializeField] public bool MouseInvertVertical { get; set; } = false;
        [field: SerializeField, Range(0f, 1f)] public float GamepadSensitivityHorizontal { get; set; } = 0.5f;
        [field: SerializeField, Range(0f, 1f)] public float GamepadSensitivityVertical { get; set; } = 0.5f;
        [field: SerializeField] public bool GamepadInvertVertical { get; set; } = false;
        
        /* ACCESSIBILITY */
        
        #region UnityEvents

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            LocalizationSystem.CurrentLanguage = CurrentLanguage;
            InputController.Instance.SetInputType(InputType);
        }

        private void OnValidate()
        {
            LocalizationSystem.CurrentLanguage = CurrentLanguage;
        }

        #endregion
    }
}
