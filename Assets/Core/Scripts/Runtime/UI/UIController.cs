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

        private CameraManager _cameraManager;
        private InputController _inputController;

        private ICharacter _currentPlayer;

        #region UnityEvents

        private void Awake()
        {
            Instance = this;

            GameManager.OnPlayerChanged += OnPlayerChangedCallback;

            ShowHUD();
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

        public void ShowHUD()
        {
            _hudController.Show();
        }

        public void HideHUD()
        {
            _hudController.Hide();
        }

        public void ShowContentWindow()
        {
            _contentWindowController.Show();
        }

        public void HideContentWindow()
        {
            _contentWindowController.Hide();
        }

        #endregion

        #region PlayerMenu



        #endregion
    }
}
