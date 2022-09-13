using System;
using System.Collections.Generic;

namespace LocalizationSystem
{
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
        
        private static bool _isInitialized;

        public static Language CurrentLanguage { get; private set; } = Language.English;
        public static IDictionary<Language, string> AllLanguages { get; } = new Dictionary<Language, string>
        {
            { Language.English, nameof(Language.English) },
            { Language.Japanese, nameof(Language.Japanese) },
        };

        public static event Action<Language> OnLanguageChanged = delegate { };

        public static void Init()
        {
            CsvLoader.LoadCsv();

            UpdateLocalizationMaps();

            _isInitialized = true;
        }

        private static void UpdateLocalizationMaps()
        {
            _localizedEN = CsvLoader.GetLocalizationMap("en");
            _localizedJP = CsvLoader.GetLocalizationMap("jp");
        }

        public static void SetLanguage(Language language)
        {
            CurrentLanguage = language;
            
            OnLanguageChanged?.Invoke(CurrentLanguage);
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
                Language.English => CsvLoader.GetLocalizationMap("en"),
                Language.Japanese => CsvLoader.GetLocalizationMap("jp"),
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

#if UNITY_EDITOR
        public static void Add(string key, string value)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            CsvLoader.LoadCsv();
            CsvLoader.Add(key, value);
            CsvLoader.LoadCsv();
            
            UpdateLocalizationMaps();
        }

        public static void Remove(string key)
        {
            CsvLoader.LoadCsv();
            CsvLoader.Remove(key);
            CsvLoader.LoadCsv();
            
            UpdateLocalizationMaps();
        }
        
        public static void Replace(string key, string value)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            CsvLoader.LoadCsv();
            CsvLoader.Edit(key, value);
            CsvLoader.LoadCsv();
            
            UpdateLocalizationMaps();
        }
#endif
    }
}