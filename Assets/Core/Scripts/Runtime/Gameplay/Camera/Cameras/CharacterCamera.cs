using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem
{
    using Entity;
    using InputSystem;
    
    public class CharacterCamera : MonoBehaviour, ICamera
    {
        [Header("References")] 
        [SerializeField] private CinemachineVirtualCameraBase _cinemachineVirtualCamera;

        [Header("Settings")] 
        [Min(0)] [SerializeField] private float _rotationSpeed;

        private InputAction _moveAction;
        private Transform _player;
        private Transform _playerModel;
        private Transform _orientation;

        #region UnityEvents

        private void Awake()
        {
            GameManager.OnPlayerChanged += OnPlayerChangedCallback;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Start()
        {
            _moveAction = InputController.Instance.GetPlayerInputAction("Move");
        }

        private void Update()
        {
            // Rotate orientation
            var transformPosition = transform.position;
            var playerPosition = _player.position;
            var viewDirection =
                playerPosition - new Vector3(transformPosition.x, playerPosition.y, transformPosition.z);
            _orientation.forward = viewDirection.normalized;

            // Rotate player object
            var movement = _moveAction.ReadValue<Vector2>();

            var inputDirection = _orientation.forward * movement.y + _orientation.right * movement.x;
            if (inputDirection != Vector3.zero)
            {
                _playerModel.forward = Vector3.Slerp(_playerModel.forward, inputDirection.normalized,
                    Time.deltaTime * _rotationSpeed);
            }
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            // Get player information
            _player = player.GetTransform();
            _playerModel = _player.Find("Model");
            _orientation = _player.Find("Orientation");
        }

        #region ICamera

        public void SetFocus(Transform transform)
        {
            _cinemachineVirtualCamera.LookAt = transform;
            _cinemachineVirtualCamera.Follow = transform;
        }

        public ICinemachineCamera GetCamera()
        {
            return _cinemachineVirtualCamera;
        }

        public void Enable()
        {
            // Set priority
            _cinemachineVirtualCamera.Priority = 1;
        }

        public void Disable()
        {
            // Set priority
            _cinemachineVirtualCamera.Priority = -1;
        }
        
        #endregion
    }
}
