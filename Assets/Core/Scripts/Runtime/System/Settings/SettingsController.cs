using System;
using UnityEngine;

namespace Settings
{
    using Core.LocalizationSystem;
    
    public class SettingsController : MonoBehaviour
    {
        public static SettingsController Instance { get; private set; }

        [field: SerializeField] public LocalizationSystem.Language CurrentLanguage { get; set; } = LocalizationSystem.Language.English;

        [field: SerializeField] public bool DisplayFPS;
        [field: SerializeField] public bool DisplayTime;
        
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

        public float GetFPS()
        {
            return 1f / Time.deltaTime;
        }
    }
}
