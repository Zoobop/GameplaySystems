using System;
using UnityEngine;

namespace LocalizationSystem
{
    [Serializable]
    public struct LocalizedString
    {
        [SerializeField] private string _key;

        public string Value => LocalizationSystem.GetLocalizedValue(_key);

        public LocalizedString(string key)
        {
            _key = key;
        }
        
        public static implicit operator string(LocalizedString localizedString)
        {
            return localizedString.Value;
        }
        
        public static implicit operator LocalizedString(string str)
        {
            return new LocalizedString(str);
        }

        public override string ToString()
        {
            return this;
        }
    }
}