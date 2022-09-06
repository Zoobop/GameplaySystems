using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class EnhancedSlider : EnhancedUI<float, Slider.SliderEvent, UnityAction<float>>
    {
        [Header("Slider")]
        [SerializeField] private float _minValue = 0f;
        [SerializeField] private float _maxValue = 1f;
        [SerializeField] private float _currentValue = 0.5f;

        private Slider _slider;
        
        #region UnityEvents

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            
            _slider.minValue = _minValue;
            _slider.maxValue = _maxValue;
            _slider.value = _currentValue;
            _slider.onValueChanged = _events;
        }

        protected override void OnValidate()
        {
            _slider = GetComponentInChildren<Slider>();
            if (_slider == null) return;

            _slider.minValue = _minValue;
            _slider.maxValue = _maxValue;
            _slider.value = _currentValue;
            _slider.onValueChanged = _events;
            
            base.OnValidate();
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

        public override void SetValue(float value)
        {
            _currentValue = value;
            _slider.value = value;
        }

        #endregion

        public void SetBounds(int min, int max)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
        }
    }
}