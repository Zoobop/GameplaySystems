using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class EnhancedToggle : EnhancedUI<bool, Toggle.ToggleEvent, UnityAction<bool>>
    {
        [SerializeField] private Image _toggleCheckmark;
        [SerializeField] private Button _toggleButton;
        [SerializeField] private bool _value;

        #region UnityEvents

        private void Awake()
        {
            _toggleButton.onClick.AddListener(ToggleValue);
        }

        protected override void OnValidate()
        {
            if (_toggleCheckmark == null) return;

            _toggleCheckmark.enabled = _value;
            _toggleButton.interactable = _isActive;
            
            base.OnValidate();
        }

        public override void AddListener(UnityAction<bool> action)
        {
            _events.AddListener(action);
        }

        public override void RemoveListener(UnityAction<bool> action)
        {
            _events.RemoveListener(action);
        }

        public override void Enable()
        {
            _toggleButton.interactable = true;
        }

        public override void Disable()
        {
            _toggleButton.interactable = false;
        }

        public override bool GetValue()
        {
            return _value;
        }

        public override void SetValue(bool value)
        {
            _value = value;
            _toggleCheckmark.enabled = value;
        }

        #endregion

        private void ToggleValue()
        {
            _value = !_value;
            _toggleCheckmark.enabled = _value;
            
            _events?.Invoke(_value);
        }

    }
}