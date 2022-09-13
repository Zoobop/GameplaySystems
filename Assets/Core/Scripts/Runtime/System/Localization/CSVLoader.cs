using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalizationSystem
{
    public static class CsvLoader
    {
        private const string CsvDirectory = "Files/Localization/";
        private static string _allLocalizationMapText;

        private const char LineSeparator = '\n';
        private const string FieldSeparator = ",";

        public static void LoadCsv()
        {
            var files = Resources.LoadAll<TextAsset>(CsvDirectory);
            var allFileText = new StringBuilder();
            foreach (var textAsset in files)
            {
                allFileText.Append(textAsset.text);
            }

            _allLocalizationMapText = allFileText.ToString();
        }

        public static IDictionary<string, string> GetLocalizationMap(string languageEncoding)
        {
            // Create map
            var localizationMap = new Dictionary<string, string>();
            
            // Get file lines
            var lines = _allLocalizationMapText.Split(LineSeparator);

            // Find language map from language encoding
            var languageEncodings = lines[0].Split(FieldSeparator);
            var languageIndex = FindKeyIndex(languageEncodings, languageEncoding);

            // Parse into dictionary
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = line.Trim('\r').Split(FieldSeparator);
                //Debug.LogWarning(string.Join(' ', fields));

                if (fields.Length > languageIndex)
                {
                    var key = fields[0];
                    
                    if (localizationMap.ContainsKey(key)) continue;

                    var value = fields[languageIndex];
                    localizationMap.Add(key, value);

                    //Debug.LogWarning($"({languageEncoding}) Key: {key} - Value: {value}");
                }
            }

            return localizationMap;
        }

#if UNITY_EDITOR
        #region EditorOnly

        public static void Add(string key, string value)
        {
            var path = GetLocalizationMapPath(key);
            var fullPath = $"{CsvDirectory}{path}";
            
            var formatted = new StringBuilder($"\n{key},{value}");
            var count = Enum.GetNames(typeof(LocalizationSystem.Language)).Length;
            for (var i = 0; i < count; i++)
            {
                formatted.Append(",");
            }
            
            File.AppendAllText(fullPath, formatted.ToString());
            
            UnityEditor.AssetDatabase.Refresh();
        }

        public static void Remove(string key)
        {
            var lines = _allLocalizationMapText.Split(LineSeparator);
            var keys = new List<string>(lines.Length);
            
            foreach (var line in lines)
            {
                keys.Add(line.Split(FieldSeparator)[0]);
            }

            var index = FindKeyIndex(keys, key);
            if (index > -1)
            {
                var path = GetLocalizationMapPath(key);
                var fullPath = $"{CsvDirectory}{path}";
                
                var newLines = lines.Where(str => str != lines[index]);
                var replaced = string.Join(LineSeparator.ToString(), newLines);
                File.WriteAllText(fullPath, replaced);
            }
        }

        public static void Edit(string key, string value)
        {
            Remove(key);
            Add(key, value);
        }

        private static string GetLocalizationMapPath(string key)
        {
            var path = string.Empty;
            foreach (var fileName in ArraySegment<string>.Empty)
            {
                if (fileName.ToUpper().Contains(key.ToUpper()))
                {
                    path = fileName;
                    break;
                }
            }

            return path;
        }
        
        #endregion
#endif
        
        #region Helpers

        private static int FindKeyIndex(IList<string> collection, in string itemToFind)
        {
            var index = -1;
            for (var i = 0; i < collection.Count; i++)
            {
                if (collection[i].Contains(itemToFind))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        #endregion
    }
}