using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    using LocalizationSystem;
    
    public class EnhancedButton : EnhancedUI<bool, Button.ButtonClickedEvent, UnityAction>
    {
        [Header("References")] 
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _buttonImage;

        [Header("Text")] 
        [SerializeField] private Color _textColor;
        [SerializeField] private Color _textColorInactive;
        [TextArea] 
        [SerializeField] private string _text;

        [Header("Button")] 
        [SerializeField] private ColorBlock _buttonColors = ColorBlock.defaultColorBlock;
        
        [Header("Border")]
        [SerializeField] private Color _buttonBorderColor;
        [SerializeField] private Color _buttonBorderColorInactive;

        private Button _button;

        #region UnityEvents
        
        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            
            // Apply colors
            _button.interactable = _isActive;
            _button.colors = _buttonColors;
            _buttonText.text = _text;
            _buttonText.color = _textColor;

            // Apply events
            _button.onClick = _events;
        }

        protected override void OnValidate()
        {
            // Assign if null
            _button = GetComponentInChildren<Button>();
            if (!_button) return;

            // Apply colors
            _button.interactable = _isActive;
            _button.colors = _buttonColors;
            _buttonText.text = _text;
            _buttonText.color = _textColor;

            // Apply events
            _button.onClick = _events;
            
            base.OnValidate();
        }

        #endregion
        
        #region EnhancedUI

        public override void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public override void RemoveListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }
        
        public override void Enable()
        {
            _button.interactable = true;
            _buttonImage.color = _buttonBorderColor;
            _buttonText.color = _textColor;
        }

        public override void Disable()
        {
            _button.interactable = false;
            _buttonImage.color = _buttonBorderColorInactive;
            _buttonText.color = _textColorInactive;
        }

        public override bool GetValue()
        {
            return _button.interactable;
        }

        public override void SetValue(bool value)
        {
            _button.interactable = value;
        }

        #endregion

        public void SetText(string text)
        {
            _text = text;
            _buttonText.text = text;
        }
    }
}