using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using RebindOperation = UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation;

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
        [SerializeField] private Image _keyBindImage;
        
        [Header("Key Bind Visuals")]
        [SerializeField] private LocalizedString _displayName;
        [SerializeField] private LocalizedString _resetButtonName;

        private KeyBind _keyBind;
        private InputType _inputType;
        private string _actionName;
        private string _keyBindName;
        private int _relativeIndex;

        #region UnityEvents

        private void Awake()
        {
            LocalizationSystem.OnLanguageChanged += OnLanguageChangedCallback;
        }

        private void OnDestroy()
        {
            LocalizationSystem.OnLanguageChanged -= OnLanguageChangedCallback;
        }

        #endregion
        
        private void UpdateUI()
        {
            // Update key bind action text
            _displayText.text = _displayName;
            _resetButtonText.text = _resetButtonName;
            
            if (_inputType == InputType.Keyboard)
            {
                //_keyBindImage.sprite = InputActionRegistry.Instance.GetKeyboardKeyImage();
                _keyBindImage.enabled = false;
                _bindButtonText.enabled = true;
                _bindButtonText.text = InputActionRegistry.EnumToString(_keyBind);
                return;
            }

            _keyBindImage.enabled = true;
            _keyBindImage.sprite = InputActionRegistry.KeyCodeToImage(_keyBind);
            _bindButtonText.enabled = false;
        }
        
        private void OnLanguageChangedCallback(LocalizationSystem.Language language)
        {
            _displayText.text = _displayName;
            _resetButtonText.text = _resetButtonName;
        }

        public void AssignBinding(string actionName, KeyBind keyBind, InputType inputType)
        {
            // Set input action
            _actionName = actionName;
            _keyBindName = actionName;
            _keyBind = keyBind;
            _inputType = inputType;
            _relativeIndex = 0;

            // Update UI
            UpdateUI();
        }
        
        public void AssignBinding(string actionName, string secondaryName, KeyBind keyBind, InputType inputType, int relativeIndex = 0)
        {
            // Set input action
            _actionName = actionName;
            _keyBindName = secondaryName;
            _keyBind = keyBind;
            _inputType = inputType;
            _relativeIndex = relativeIndex;

            // Update UI
            UpdateUI();
        }

        public void RebindKeyBind(Action action)
        {
            var bindIndex = (int) _inputType + _relativeIndex;
            InputController.RebindKeyBind(_actionName, _keyBindName, bindIndex, action);
        }

        public void ResetKeyBind()
        {
            // Reset input action
            var bindIndex = (int) _inputType + _relativeIndex;
            InputController.ResetKeyBind(_actionName, _keyBindName, bindIndex);
        }
    }
}
