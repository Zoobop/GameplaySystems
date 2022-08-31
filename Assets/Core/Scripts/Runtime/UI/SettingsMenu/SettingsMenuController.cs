using System.Collections;
using UnityEngine;

namespace UI
{
    using InputSystem;
    
    public class SettingsMenuController : Controller
    {
        public static SettingsMenuController Instance { get; private set; }
        
        [Header("Settings Menus")] 
        [SerializeField] private TabController _tabController;
        [SerializeField] private GeneralSettingsUI _generalSettings;
        [SerializeField] private GraphicsSettingsUI _graphicsSettings;
        [SerializeField] private AudioSettingsUI _audioSettings;
        [SerializeField] private ControlsSettingsUI _controlsSettings;
        [SerializeField] private AccessibilitySettingsUI _accessibilitySettings;

        [Header("Transition")] 
        [SerializeField] private CanvasGroup _transition;

        [SerializeField] private float _transitionFadeTime = 0.2f;
        [SerializeField] private float _tickSpeed = 0.001f;

        private WaitForSeconds _tickDelay;
        private WaitForSeconds _transitionDelay;
        private bool _isOpen;
        
        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            Instance = this;
            
            _tickDelay = new WaitForSeconds(_tickSpeed);
            _transitionDelay = new WaitForSeconds(0.15f);
        }

        #endregion

        #region MenuUtility

        public void OpenMenu()
        {
            Show();
            _isOpen = true;

            InputController.Instance.DisableMovementActions();
            InputController.Instance.DisableInteractionActions();
            InputController.Instance.SetCurrentTabController(_tabController);
        }
        
        public void CloseMenu()
        {
            Hide();
            _isOpen = false;
            
            InputController.Instance.RemoveCurrentTabController();
            InputController.Instance.EnableMovementActions();
            InputController.Instance.EnableInteractionActions();
        }

        public void Toggle()
        {
            if (!_isOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        protected override IEnumerator AwaitOnEndShow()
        {
            yield return _transitionDelay;
            
            var elapsedTime = 0f;
            while (elapsedTime < _transitionFadeTime)
            {
                Transition(0, 1, ref elapsedTime);

                yield return _tickDelay;
            }
        }
        
        protected override IEnumerator AwaitOnBeginHide()
        {
            var elapsedTime = 0f;
            while (elapsedTime < _transitionFadeTime)
            {
                Transition(1, 0, ref elapsedTime);

                yield return _tickDelay;
            }
            
            yield return _transitionDelay;
        }

        private void Transition(float start, float end, ref float elapsed)
        {
            elapsed += Time.deltaTime;
            _transition.alpha = Mathf.Lerp(start, end, elapsed / _transitionFadeTime);
        }
        
        #endregion
    }
}