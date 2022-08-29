using System.Collections.Generic;
using TMPro;
using UnityEngine;

using InputSystem;

namespace UI
{
    public class BindDropdownOptions : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _bindOptions;

        private List<KeyBind> _keyCodes;

        private void Awake()
        {
            GetBindOptions();
        }

        private void GetBindOptions()
        {
            // Get valid key codes from registry
            _keyCodes = InputActionRegistry.Instance.GetKeyCodes();

            // Clear options to start fresh
            _bindOptions.options.Clear();

            // Add key codes to options list
            //var options = _keyCodes.ConvertAll(element =>
            //    new TMP_Dropdown.OptionData(InputActionRegistry.NaturalString(element)));
            //_bindOptions.options = options;
        }

        public void SetOption(KeyBind key)
        {
            // Set value by key index
            _bindOptions.value = _keyCodes.IndexOf(key);
        }

        public KeyBind GetSelectedOption()
        {
            // Get key by index
            return _keyCodes[_bindOptions.value];
        }
    }
}
