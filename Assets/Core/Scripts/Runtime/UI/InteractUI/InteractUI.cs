using TMPro;
using UnityEngine;

using CameraSystem;
using InputSystem;

namespace UI
{
    public class InteractUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _interactionText;

        private Canvas _canvas;
        private Camera _camera;
        private KeyCode _key;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            InputController.Instance.OnKeyBindsChanged += UpdateUI;
            _camera = CameraManager.Main;
            _canvas.worldCamera = _camera;

            UpdateUI(InputController.Instance.CurrentKeyBinds);
        }

        private void LateUpdate()
        {
            // Return if null
            if (!_camera) return;

            // Billboard effect
            transform.LookAt(_camera.transform);
            transform.Rotate(0, 180, 0);
        }

        private void UpdateUI(KeyBindings keyBindings)
        {
            //_key = keyBindings.InteractKeyBind;
            //_interactionText.text = $"{InputActionRegistry.NaturalString(_key)}";
        }
    }
}
