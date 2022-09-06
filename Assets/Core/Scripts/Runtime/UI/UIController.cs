using UnityEngine;

namespace UI
{
    using Entity;
    using CameraSystem;
    using InputSystem;
    
    public class UIController : MonoBehaviour
    {
        public static UIController Instance { get; private set; }

        [SerializeField] private HUDController _hudController;
        [SerializeField] private ContentWindowsController _contentWindowController;
        [SerializeField] private PlayerMenuController _playerMenuController;
        [SerializeField] private SettingsMenuController _settingsMenuController;
        [SerializeField] private GameObject _settingsDisplay;

        private CameraManager _cameraManager;
        private InputController _inputController;

        private ICharacter _currentPlayer;

        #region UnityEvents

        private void Awake()
        {
            Instance = this;

            GameManager.OnPlayerChanged += OnPlayerChangedCallback;
        }

        private void Start()
        {
            _cameraManager = CameraManager.Instance;
            _inputController = InputController.Instance;
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            // Assign player
            _currentPlayer = player;

            // Bind player stats to UI
            _hudController.Bind(player);
            _contentWindowController.Bind(player);
            _playerMenuController.Bind(player);
        }

        #region UIDisplay

        public static void ShowHUD()
        {
            Instance._hudController.Show();
        }

        public static void HideHUD()
        {
            Instance._hudController.Hide();
        }

        public void ShowContentWindow()
        {
            Instance._contentWindowController.Show();
        }

        public void HideContentWindow()
        {
            Instance._contentWindowController.Hide();
        }

        public void ShowSettingsMenu()
        {
            Instance._settingsMenuController.Show();
        }
        
        public void HideSettingsMenu()
        {
            Instance._settingsMenuController.Hide();
        }

        #endregion

        #region Utility

        public static void EnableUI()
        {
            Instance.SetUIStatus(true);
        }

        public static void DisableUI()
        {
            Instance.SetUIStatus(false);
        }

        private void SetUIStatus(bool state)
        {
            _hudController.gameObject.SetActive(state);
            _contentWindowController.gameObject.SetActive(state);
            _playerMenuController.gameObject.SetActive(state);
            _settingsMenuController.gameObject.SetActive(state);
            _settingsDisplay.SetActive(state);
        }

        #endregion
    }
}
