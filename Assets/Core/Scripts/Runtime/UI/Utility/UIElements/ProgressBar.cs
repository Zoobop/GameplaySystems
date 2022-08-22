using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using Entity;
    
    public class ProgressBar : MonoBehaviour
    {
        private enum BarOptions : byte
        {
            SingleBar,
            DoubleBar,
        }

        private enum TextOptions : byte
        {
            None = 0,
            InterpolateText = 1,
        }

        [Flags]
        private enum FillOptions : byte
        {
            None = 0,
            FillWrap = 1,
        }

        [Header("References")] [SerializeField]
        private Image _primaryFillBarMask;

        [SerializeField] private Image _primaryFillBar;
        [SerializeField] private Image _secondaryFillBarMask;
        [SerializeField] private Image _secondaryFillBar;
        [SerializeField] private TextMeshProUGUI _ratioText;

        [Header("Fill Bars")] [SerializeField] private Color _primaryFillColor = Color.grey;
        [SerializeField] private Color _secondaryFillColor = Color.grey;
        [Min(0)] [SerializeField] private float _primaryBarFillTime = 1f;
        [Min(0)] [SerializeField] private float _secondaryBarFillTime = 0.5f;
        [Min(0)] [SerializeField] private float _fillDelay = 0.75f;

        [Header("Options")] [SerializeField] private BarOptions _barOptions = BarOptions.SingleBar;

        [SerializeField] private TextOptions _textOptions = TextOptions.InterpolateText;
        //[SerializeField] private FillOptions _fillOptions = FillOptions.None;

        //private readonly string RatioFormat = "{0:F2} / {1:F2}";
        //private readonly string DoublePercentageFormat = "{0:F2}%";
        //private readonly string SinglePercentageFormat = "{0}%";
        //private readonly string CurrentFormat = "{0}";

        private WaitForSeconds _delay;
        private Coroutine _coroutine;
        private IEnumerator _increaseFillCoroutine;
        private IEnumerator _decreaseFillCoroutine;

        private StatAttributeElement _statAttributeElement;
        private int _oldCurrent;

        #region UnityEvents

        private void Awake()
        {
            _primaryFillBar.color = _primaryFillColor;
            _secondaryFillBar.color = _secondaryFillColor;
            _delay = new WaitForSeconds(_fillDelay);
            _oldCurrent = -1;

            _ratioText.enabled = _textOptions != TextOptions.None;
            ApplyBarOptions();
        }

        private void OnValidate()
        {
            _primaryFillBar.color = _primaryFillColor;
            _secondaryFillBar.color = _secondaryFillColor;
            _delay = new WaitForSeconds(_fillDelay);
            _ratioText.enabled = _textOptions != TextOptions.None;

            ApplyBarOptions();
        }

        #endregion

        #region Utility

        public void Bind(StatAttributeElement statAttributeElement)
        {
            // Assign new stat attribute
            _statAttributeElement = statAttributeElement;

            // Modification difference
            var difference = _oldCurrent - _statAttributeElement.Current;

            // Update old current
            _oldCurrent = _statAttributeElement.Current;

            // Get appropriate coroutines
            (_increaseFillCoroutine, _decreaseFillCoroutine) = ApplyBarOptions();

            // Check for modification increase or decrease
            if (difference > 0)
            {
                // Stop active coroutine
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                // Decrease
                _coroutine = StartCoroutine(_decreaseFillCoroutine);
            }
            else if (difference < 0)
            {
                // Stop active coroutine
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                // Increase
                _coroutine = StartCoroutine(_increaseFillCoroutine);
            }
        }

        private IEnumerator AnimateFillBarSingle()
        {
            // Get percentage
            var currentPercentage = _primaryFillBarMask.fillAmount;
            var newPercentage = _statAttributeElement.Ratio;

            if (!_textOptions.HasFlag(TextOptions.InterpolateText))
            {
                _ratioText.text =
                    $"{newPercentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
            }

            // Interpolate foreground bar
            var elapsedTime = 0f;
            while (elapsedTime <= _primaryBarFillTime)
            {
                elapsedTime += Time.deltaTime;
                var percentage = Mathf.Lerp(currentPercentage, newPercentage,
                    elapsedTime / _primaryBarFillTime);
                _primaryFillBarMask.fillAmount = percentage;

                if (_textOptions.HasFlag(TextOptions.InterpolateText))
                {
                    // Interpolate text
                    _ratioText.text =
                        $"{percentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
                }

                yield return null;
            }

            // Ensure new percentage value is reached
            _primaryFillBarMask.fillAmount = newPercentage;
            _ratioText.text =
                $"{newPercentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
        }

        private IEnumerator AnimateFillBarIncrease()
        {
            var currentForegroundPercentage = _primaryFillBarMask.fillAmount;
            var currentBackgroundPercentage = _secondaryFillBarMask.fillAmount;
            var newPercentage = _statAttributeElement.Ratio;

            if (!_textOptions.HasFlag(TextOptions.InterpolateText))
            {
                _ratioText.text =
                    $"{newPercentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
            }

            // Interpolate background bar
            var elapsedTime = 0f;
            while (elapsedTime <= _secondaryBarFillTime)
            {
                elapsedTime += Time.deltaTime;
                var percentage = Mathf.Lerp(currentBackgroundPercentage, newPercentage,
                    elapsedTime / _secondaryBarFillTime);
                _secondaryFillBarMask.fillAmount = percentage;

                if (_textOptions.HasFlag(TextOptions.InterpolateText))
                {
                    // Interpolate text
                    _ratioText.text =
                        $"{percentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
                }

                yield return null;
            }

            // Ensure new percentage value is reached
            _secondaryFillBarMask.fillAmount = newPercentage;
            _ratioText.text =
                $"{newPercentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";

            // Delay before interpolation
            yield return _delay;

            // Interpolate foreground bar
            elapsedTime = 0f;
            while (elapsedTime <= _primaryBarFillTime)
            {
                elapsedTime += Time.deltaTime;
                var percentage = Mathf.Lerp(currentForegroundPercentage, newPercentage,
                    elapsedTime / _primaryBarFillTime);
                _primaryFillBarMask.fillAmount = percentage;
                yield return null;
            }

            // Ensure new percentage value is reached
            _primaryFillBarMask.fillAmount = newPercentage;
        }

        private IEnumerator AnimateFillBarDecrease()
        {
            var currentForegroundPercentage = _primaryFillBarMask.fillAmount;
            var currentBackgroundPercentage = _secondaryFillBarMask.fillAmount;
            var newPercentage = _statAttributeElement.Ratio;

            if (!_textOptions.HasFlag(TextOptions.InterpolateText))
            {
                _ratioText.text =
                    $"{newPercentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
            }

            // Interpolate background bar
            var elapsedTime = 0f;
            while (elapsedTime <= _secondaryBarFillTime)
            {
                elapsedTime += Time.deltaTime;
                var percentage = Mathf.Lerp(currentForegroundPercentage, newPercentage,
                    elapsedTime / _secondaryBarFillTime);
                _primaryFillBarMask.fillAmount = percentage;

                if (_textOptions.HasFlag(TextOptions.InterpolateText))
                {
                    // Interpolate text
                    _ratioText.text =
                        $"{percentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";
                }

                yield return null;
            }

            // Ensure new percentage value is reached
            _primaryFillBarMask.fillAmount = newPercentage;
            _ratioText.text =
                $"{newPercentage * _statAttributeElement.Maximum:F1} / {_statAttributeElement.Maximum:F1}";

            // Delay before interpolation
            yield return _delay;

            // Interpolate foreground bar
            elapsedTime = 0f;
            while (elapsedTime <= _primaryBarFillTime)
            {
                elapsedTime += Time.deltaTime;
                var percentage = Mathf.Lerp(currentBackgroundPercentage, newPercentage,
                    elapsedTime / _primaryBarFillTime);
                _secondaryFillBarMask.fillAmount = percentage;
                yield return null;
            }

            // Ensure new percentage value is reached
            _secondaryFillBarMask.fillAmount = newPercentage;
        }

        #endregion

        #region Setup

        private (IEnumerator, IEnumerator) ApplyBarOptions()
        {
            // Single bar
            if (_barOptions == BarOptions.SingleBar)
            {
                _secondaryFillBarMask.enabled = false;
                _secondaryFillBar.enabled = false;

                return (AnimateFillBarSingle(), AnimateFillBarSingle());
            }

            // Double bar
            _secondaryFillBarMask.enabled = true;
            _secondaryFillBar.enabled = true;

            return (AnimateFillBarIncrease(), AnimateFillBarDecrease());
        }

        #endregion

    }
}
