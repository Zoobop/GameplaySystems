using TMPro;
using UnityEngine;

using InputSystem;

namespace UI
{
    public class RebindKeyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _keyBindNameText;
        [SerializeField] private BindDropdownOptions _bindOptions;

        private InputController _inputController;
        private KeyBind _keyBind;
        private InputType _inputType;
        private string _actionName;
        private string _displayName;

        private void Start()
        {
            _inputController = InputController.Instance;
        }

        private void UpdateUI()
        {
            // Update key bind action text
            _keyBindNameText.text = _displayName;

            // Update dropdown value
            _bindOptions.SetOption(_keyBind);
        }

        public void AssignBinding(string actionName, string displayName, KeyBind key, InputType inputType)
        {
            // Set input action
            _actionName = actionName;
            _displayName = displayName;
            _keyBind = key;
            _inputType = inputType;

            // Update UI
            UpdateUI();
        }

        public void UpdateKeyBinds()
        {
            // Get key bind from (options) value
            var keyBind = _bindOptions.GetSelectedOption();

            // Update key bind
            InputController.Instance.SetKeyBind(_actionName, keyBind, _inputType);

        }

        public void ResetKeyBind()
        {
            // Reset input action
            _keyBind = _inputController.ResetKeyBind(_actionName, _inputType);

            // Update key bind
            UpdateUI();
        }
    }
}
