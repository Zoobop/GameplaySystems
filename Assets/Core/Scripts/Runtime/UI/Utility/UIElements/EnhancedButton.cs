using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class EnhancedButton : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI _buttonText;

        [SerializeField] private Image _buttonBorder;

        [Header("Events")] [SerializeField] protected Button.ButtonClickedEvent _events;

        [Header("Text")] [SerializeField] private Color _textColor;
        [SerializeField] private Color _textColorInactive;
        [TextArea] [SerializeField] private string _text;

        [Header("Button")] [SerializeField] private bool _active = true;
        [SerializeField] private Color _buttonBorderColor;
        [SerializeField] private Color _buttonBorderColorInactive;
        [SerializeField] private Color _buttonColor;
        [SerializeField] private Color _buttonColorInactive;

        protected Button _button;

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

        private void OnValidate()
        {
            // Assign if null
            _button ??= GetComponentInChildren<Button>();
            if (!_button) return;

            // Apply colors
            var color = ColorUtils.ApplyColor(_buttonColor);
            color.disabledColor = _buttonColorInactive;
            _button.colors = color;
            _buttonBorder.color = _buttonBorderColor;
            _buttonText.text = _text;
            _buttonText.color = _textColor;
            _button.interactable = _active;

            if (_active)
            {
                Enable();
            }
            else
            {
                Disable();
            }

            // Apply events
            _button.onClick = _events;
        }

        public void AddEvent(UnityAction action)
        {
            _events.AddListener(action);
        }

        public void RemoveEvent(UnityAction action)
        {
            _events.RemoveListener(action);
        }

        public void SetText(string text)
        {
            _buttonText.text = text;
        }

        public void Enable()
        {
            _button.enabled = true;
            _buttonBorder.color = _buttonBorderColor;
            _buttonText.color = _textColor;
        }

        public void Disable()
        {
            _button.enabled = false;
            _buttonBorder.color = _buttonBorderColorInactive;
            _buttonText.color = _textColorInactive;
        }
    }
}