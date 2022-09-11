using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    using Utility.ExtensionMethods;
    
    public class EnhancedDropdown : EnhancedUI<int, Dropdown.DropdownEvent, UnityAction<int>>
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI _placeholderText;

        [Header("Dropdown Options")] [SerializeField]
        private List<string> _options = new();

        private TMP_Dropdown _dropdown;

        private static readonly string PlaceHolderText = "All";

        #region UnityEvents

        private void Awake()
        {
            _dropdown = GetComponentInChildren<TMP_Dropdown>();
            
            if (_options.IsEmpty()) _placeholderText.text = "N/A";

            // Refresh options
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_options);
        }

        protected override void OnValidate()
        {
            // Assign if null
            _dropdown ??= GetComponentInChildren<TMP_Dropdown>();

            if (_options.IsEmpty()) _placeholderText.text = "N/A";

            // Refresh options
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_options);
            
            base.OnValidate();
        }

        #endregion

        #region EnhancedUI

        public override void AddListener(UnityAction<int> action)
        {
            _dropdown.onValueChanged.AddListener(action);
        }

        public override void RemoveListener(UnityAction<int> action)
        {
            _dropdown.onValueChanged.RemoveListener(action);
        }
        
        public override void Enable()
        {
            _dropdown.interactable = true;
        }

        public override void Disable()
        {
            _dropdown.interactable = false;
        }

        public override int GetValue()
        {
            return _dropdown.value;
        }

        public override void SetValue(int value)
        {
            _dropdown.value = value;
        }

        #endregion

        public string GetSelectedOption()
        {
            return _dropdown.options[_dropdown.value].text;
        }

        public List<string> GetCurrentOptions()
        {
            return _dropdown.options.ConvertAll(data => data.text);
        }

        public void SetPlaceholderText(string text)
        {
            _placeholderText.text = text;
        }

        public void ResetPlaceholderText()
        {
            _placeholderText.text = PlaceHolderText;
        }

        public void Apply(int value = 0)
        {
            _dropdown.value = value;
            _placeholderText.text = _dropdown.options[value].text;
        }
        
        public void SetOptions(List<string> options)
        {
            // Get difference in length
            var difference = _dropdown.options.Count - options.Count;
            var newCount = Mathf.Abs(difference);

            // Less options
            if (difference > 0)
            {
                _dropdown.options.RemoveRange(options.Count, newCount);
            }
            // Gain options
            else if (difference < 0)
            {
                // Update options
                var count = _dropdown.options.Count + newCount;
                for (var i = _dropdown.options.Count; i < count; i++)
                {
                    _dropdown.options.Add(new TMP_Dropdown.OptionData {text = options[i]});
                }
            }

            // Update selected value to be within range if necessary
            if (_dropdown.value >= _dropdown.options.Count)
            {
                _dropdown.SetValueWithoutNotify(_dropdown.options.Count - 1);
            }

            //print($"[{string.Join(" - ", _dropdown.options.ConvertAll(data => data.text))}]");
        }

        public void SetOptionsText(IList<string> texts)
        {
            for (var i = 0; i < _dropdown.options.Count; i++)
            {
                _dropdown.options[i].text = texts[i];
            }
            
            SetPlaceholderText(texts[_dropdown.value]);
        }

        public bool HaveOptionsChanged(List<string> options)
        {
            var current = _dropdown.options.ConvertAll(data => data.text);
            return !current.IsIdentical(options);
        }
    }
}
