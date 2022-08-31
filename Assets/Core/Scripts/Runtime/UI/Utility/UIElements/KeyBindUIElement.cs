using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using InputSystem;
    
    public class KeyBindUIElement : MonoBehaviour
    {
        [Header("Key Bind UI Mapping")]
        [SerializeField] private TextMeshProUGUI _keyboardKeyText;
        [SerializeField] private Image _keyBindImage;
        [SerializeField] private string _actionName;

        #region UnityEvents

        private void Start()
        {
            InputController.Instance.OnKeyBindsChanged += Bind;
            Bind(InputController.Instance.CurrentKeyBinds);
        }

        #endregion

        private void Bind(KeyBindings bindings)
        {
            var key = bindings[_actionName];
            
            // Not keyboard
            if (InputController.Instance.InputType != InputType.Keyboard)
            {
                _keyboardKeyText.enabled = false;
                _keyBindImage.sprite = InputActionRegistry.Instance.KeyCodeToImage(key);
                return;
            }

            // Keyboard
            _keyBindImage.sprite = InputActionRegistry.Instance.GetKeyboardKeyImage();
            _keyboardKeyText.enabled = true;
            _keyboardKeyText.text = InputActionRegistry.EnumToString(key);
        }
    }
}
