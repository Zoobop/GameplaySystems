using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SettingsMenuController : Controller
    {
        [Header("Settings Menus")] 
        [SerializeField] private TabController _tabController;
        [SerializeField] private GeneralSettingsUI _generalSettings;
        [SerializeField] private GraphicsSettingsUI _graphicsSettings;
        [SerializeField] private AudioSettingsUI _audioSettings;
        [SerializeField] private ControlsSettingsUI _controlsSettings;
        [SerializeField] private AccessibilitySettingsUI _accessibilitySettings;

        public void OpenMenu()
        {
            Show();
        }
        
        public void CloseMenu()
        {
            Hide();
        }

        protected override void OnEndShow()
        {
            _tabController.gameObject.SetActive(true);
        }

        protected override void OnBeginHide()
        {
            _tabController.gameObject.SetActive(false);
        }
    }
}