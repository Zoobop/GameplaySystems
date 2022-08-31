using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class EnhancedToggle : EnhancedUI<bool, Button.ButtonClickedEvent, UnityAction>
    {
        [SerializeField] private Image _toggleCheckmark;
        [SerializeField] private Button _toggleButton;
        [SerializeField] private bool _value;

        #region UnityEvents

        private void Awake()
        {
            _toggleButton.onClick = _events;
            _toggleButton.onClick.AddListener(ToggleValue);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_toggleCheckmark != null)
            {
                _toggleCheckmark.enabled = _value;
            }
        }

        public override void AddListener(UnityAction action)
        {
            _toggleButton.onClick.AddListener(action);
        }

        public override void RemoveListener(UnityAction action)
        {
            _toggleButton.onClick.RemoveListener(action);
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
        
        #endregion

        private void ToggleValue()
        {
            _value = !_value;
            _toggleCheckmark.enabled = _value;
        }

    }
}