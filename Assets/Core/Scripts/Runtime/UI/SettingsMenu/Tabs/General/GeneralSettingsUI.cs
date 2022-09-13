using System;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public class GeneralSettingsUI : BaseSettingsUI
    {
        [SerializeField] private EnhancedButton _exitButton;

        private void Awake()
        {
            _exitButton.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            _exitButton.RemoveListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit(0);
        }
    }
}