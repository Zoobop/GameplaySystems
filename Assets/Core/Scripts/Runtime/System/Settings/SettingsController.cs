using System;
using UnityEngine;

namespace Settings
{
    using LocalizationSystem;
    
    public class SettingsController : MonoBehaviour
    {
        public static SettingsController Instance { get; private set; }

        /* GENERAL */
        [field: SerializeField] public LocalizationSystem.Language CurrentLanguage { get; set; } = LocalizationSystem.Language.English;
        
        /* GRAPHICS */
        [field: SerializeField] public bool DisplayFPS;
        [field: SerializeField] public bool DisplayTime;

        /* AUDIO */
        
        /* CONTROLS */
        
        /* ACCESSIBILITY */
        
        #region UnityEvents

        private void Awake()
        {
            Instance = this;
        }

        private void OnValidate()
        {
            LocalizationSystem.CurrentLanguage = CurrentLanguage;
        }

        #endregion
    }
}
