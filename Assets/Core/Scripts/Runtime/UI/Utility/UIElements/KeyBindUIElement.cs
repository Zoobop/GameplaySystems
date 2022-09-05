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
            InputController.OnKeyBindsChanged += Bind;
            Bind(InputController.CurrentKeyBinds);
        }

        #endregion

        private void Bind(KeyBindings bindings)
        {
            var key = bindings[_actionName];
            
            // Not keyboard
            if (InputController.InputType != InputType.Keyboard)
            {
                _keyboardKeyText.enabled = false;
                _keyBindImage.sprite = InputActionRegistry.KeyCodeToImage(key);
                return;
            }

            // Keyboard
            _keyBindImage.sprite = InputActionRegistry.GetKeyboardKeyImage();
            _keyboardKeyText.enabled = true;
            _keyboardKeyText.text = InputActionRegistry.EnumToString(key);
        }
    }
}
