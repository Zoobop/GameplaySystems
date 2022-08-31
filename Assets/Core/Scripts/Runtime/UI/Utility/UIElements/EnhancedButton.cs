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
        [SerializeField] private Image _buttonBorder;

        [Header("Text")] 
        [SerializeField] private Color _textColor;
        [SerializeField] private Color _textColorInactive;
        [TextArea] 
        [SerializeField] private string _text;

        [Header("Button")]
        [SerializeField] private Color _buttonBorderColor;
        [SerializeField] private Color _buttonBorderColorInactive;
        [SerializeField] private Color _buttonColor;
        [SerializeField] private Color _buttonColorInactive;

        private Button _button;

        #region UnityEvents
        
        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            var color = ColorUtils.ApplyColor(_buttonColor);
            color.disabledColor = _buttonColorInactive;
            _button.colors = color;
            _buttonBorder.color = _buttonBorderColor;
            _button.onClick = _events;
            _buttonText.text = _text;
            _buttonText.color = _textColor;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            // Assign if null
            _button = GetComponentInChildren<Button>();
            if (!_button) return;

            // Apply colors
            var color = ColorUtils.ApplyColor(_buttonColor);
            color.disabledColor = _buttonColorInactive;
            _button.colors = color;
            _buttonBorder.color = _buttonBorderColor;
            _buttonText.text = _text;
            _buttonText.color = _textColor;
            _button.interactable = _isActive;

            // Apply events
            _button.onClick = _events;
        }

        #endregion
        
        #region EnhancedUI

        public override void AddListener(UnityAction action)
        {
            _events.AddListener(action);
        }

        public override void RemoveListener(UnityAction action)
        {
            _events.RemoveListener(action);
        }
        
        public override void Enable()
        {
            _button.enabled = true;
            _buttonBorder.color = _buttonBorderColor;
            _buttonText.color = _textColor;
        }

        public override void Disable()
        {
            _button.enabled = false;
            _buttonBorder.color = _buttonBorderColorInactive;
            _buttonText.color = _textColorInactive;
        }

        public override bool GetValue()
        {
            return _button.interactable;
        }

        #endregion

        public void SetText(string text)
        {
            _text = text;
            _buttonText.text = text;
        }
    }
}