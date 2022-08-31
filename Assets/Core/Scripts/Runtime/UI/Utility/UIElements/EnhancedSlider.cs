using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class EnhancedSlider : EnhancedUI<float, Slider.SliderEvent, UnityAction<float>>
    {
        [Header("Slider")]
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _currentValue;

        private Slider _slider;
        
        #region UnityEvents

        protected void Awake()
        {
            
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            _slider = GetComponentInChildren<Slider>();
            if (_slider == null) return;

            _slider.minValue = _minValue;
            _slider.maxValue = _maxValue;
            _slider.value = _currentValue;

            _slider.onValueChanged = _events;
        }

        #endregion

        #region EnhancedUI

        public override void AddListener(UnityAction<float> action)
        {
            _slider.onValueChanged.AddListener(action);
        }

        public override void RemoveListener(UnityAction<float> action)
        {
            _slider.onValueChanged.RemoveListener(action);
        }

        public override void Enable()
        {
            _slider.interactable = true;
        }

        public override void Disable()
        {
            _slider.interactable = false;
        }
        
        public override float GetValue()
        {
            return _currentValue;
        }

        #endregion
    }
}