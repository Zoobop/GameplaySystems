using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

using Entity;
using InputSystem;

namespace CameraSystem
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private CinemachineVirtualCameraBase _cinemachineVirtualCamera;

        [Header("Settings")] [Range(0, 5)] [SerializeField]
        private float _horizontalSensitivity = 5f;

        [Range(0, 5)] [SerializeField] private float _verticalSensitivity = 5f;

        private bool _isActive;
        private Transform _player;
        private Transform _playerModel;
        private Transform _orientation;
        private FPCameraPosition _fpCameraPosition;

        private InputAction _lookAction;
        private float _xRotation;
        private float _yRotation;

        #region UnityEvents

        private void Awake()
        {
            GameManager.OnPlayerChanged += SetPlayerData;
        }

        private void Start()
        {
            // Assign if null
            _lookAction ??= InputController.GetPlayerInputAction("Look1stPerson");
        }

        private void Update()
        {
            // Set camera position to camera holder
            transform.position = _fpCameraPosition.CameraPosition;
            transform.forward = _orientation.forward;

            // Return if not active
            if (!_isActive) return;

            // Get delta values
            var delta = _lookAction.ReadValue<Vector2>();

            _yRotation += delta.x * (_horizontalSensitivity / 10f);
            _xRotation -= delta.y * (_verticalSensitivity / 10f);

            _xRotation = Mathf.Clamp(_xRotation, -88f, 80f);

            // Rotate camera and orientation
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
            _playerModel.rotation = _orientation.rotation;
        }

        #endregion

        private void SetPlayerData(ICharacter player)
        {
            // Get player information
            _player = player.GetTransform();
            _playerModel = _player.Find("Model");
            _orientation = _player.Find("Orientation");
            _fpCameraPosition = _player.Find("FPCameraPosition").GetComponent<FPCameraPosition>();
            // Set camera position to camera holder
            transform.position = _fpCameraPosition.CameraPosition;
        }

        private void ApplySettings()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public ICinemachineCamera GetCamera()
        {
            return _cinemachineVirtualCamera;
        }

        public void Enable()
        {
            // Set priority
            _cinemachineVirtualCamera.Priority = 1;
            _isActive = true;

            ApplySettings();
        }

        public void Disable()
        {
            // Set priority
            _cinemachineVirtualCamera.Priority = -1;
            _isActive = false;
        }
    }
}
