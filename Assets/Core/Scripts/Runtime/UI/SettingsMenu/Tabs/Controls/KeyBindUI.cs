using System;
using TMPro;
using UnityEngine;

namespace UI
{
    using InputSystem;
    using LocalizationSystem;
    
    public class KeyBindUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _displayText;
        [SerializeField] private TMP_Text _bindButtonText;
        [SerializeField] private TMP_Text _resetButtonText;
        
        [Header("Key Bind Visuals")]
        [SerializeField] private LocalizedString _displayName;
        [SerializeField] private LocalizedString _keyBindName;
        [SerializeField] private LocalizedString _resetButtonName;

        private KeyBind _keyBind;
        private InputType _inputType;
        private string _actionName;

        private void UpdateUI()
        {
            // Update key bind action text
            _displayText.text = _displayName;
            _bindButtonText.text = _keyBindName;
            _resetButtonText.text = _resetButtonName;
        }

        public void AssignBinding(string actionName, KeyBind key, InputType inputType)
        {
            // Set input action
            _actionName = actionName;
            _keyBind = key;
            _inputType = inputType;

            // Update UI
            UpdateUI();
        }

        public void UpdateKeyBinds()
        {
            // Update key bind
            InputController.Instance.SetKeyBind(_actionName, _keyBind, _inputType);
        }

        public void ResetKeyBind()
        {
            // Reset input action
            _keyBind = InputController.Instance.ResetKeyBind(_actionName, _inputType);

            // Update key bind
            UpdateUI();
        }
    }
}
