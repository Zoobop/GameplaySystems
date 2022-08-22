using System;
using System.Collections.Generic;

namespace Core.LocalizationSystem
{
    using Entity;
    
    public static class LocalizationSystem
    {
        public enum Language
        {
            English,
            Japanese
        }

        // Language mappings
        private static IDictionary<string, string> _localizedEN;
        private static IDictionary<string, string> _localizedJP;

        private static CSVLoader _csvLoader;
        private static bool _isInitialized;

        public static Language CurrentLanguage { get; set; } = Language.English;

        public static void Init()
        {
            _csvLoader = new CSVLoader();
            _csvLoader.LoadCSV();

            UpdateLocalizationMaps();

            _isInitialized = true;
        }

        private static void UpdateLocalizationMaps()
        {
            _localizedEN = _csvLoader.GetLocalizationMap("en");
            _localizedJP = _csvLoader.GetLocalizationMap("jp");
        }

        public static string GetLocalizedValue(string key)
        {
            // Initialize if not
            if (!_isInitialized)
            {
                Init();
            }

            return CurrentLanguage switch
            {
                Language.English => TryGetValue(ref _localizedEN, key),
                Language.Japanese => TryGetValue(ref _localizedJP, key),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static IDictionary<string, string> GetLocalizedMap()
        {
            if (!_isInitialized)
            {
                Init();
            }

            return CurrentLanguage switch
            {
                Language.English => _csvLoader.GetLocalizationMap("en"),
                Language.Japanese => _csvLoader.GetLocalizationMap("jp"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static string TryGetValue(ref IDictionary<string, string> localizeMap, string key)
        {
            if (localizeMap.TryGetValue(key, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        public static void Add(string key, string value)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            // Instantiate if null
            _csvLoader ??= new CSVLoader();
            
            _csvLoader.LoadCSV();
            _csvLoader.Add(key, value);
            _csvLoader.LoadCSV();
            
            UpdateLocalizationMaps();
        }

        public static void Remove(string key)
        {
            // Instantiate if null
            _csvLoader ??= new CSVLoader();
            
            _csvLoader.LoadCSV();
            _csvLoader.Remove(key);
            _csvLoader.LoadCSV();
            
            UpdateLocalizationMaps();
        }
        
        public static void Replace(string key, string value)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            // Instantiate if null
            _csvLoader ??= new CSVLoader();
            
            _csvLoader.LoadCSV();
            _csvLoader.Edit(key, value);
            _csvLoader.LoadCSV();
            
            UpdateLocalizationMaps();
        }
    }
}