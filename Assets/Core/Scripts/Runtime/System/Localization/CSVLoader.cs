using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.LocalizationSystem
{
    public class CSVLoader
    {
        private const string CsvDirectory = @"Assets/Resources/Files/Localization/";
        private static IEnumerable<string> _fileNames = new List<string>();
        private string _allLocalizationMapText;
        
        private readonly char _lineSeperator = '\n';
        private readonly string _fieldSeperator = ",";

        public void LoadCSV()
        {
            _fileNames = Directory.GetFiles(CsvDirectory);
            var allFileText = new StringBuilder();
            foreach (var fileName in _fileNames)
            {
                if (!fileName.EndsWith(".csv")) continue;
                var properPath = fileName.Replace(@"Assets/Resources/", "")[..^4];
                var textAsset = Resources.Load<TextAsset>(properPath);
                allFileText.Append(textAsset.text);
            }

            _allLocalizationMapText = allFileText.ToString();
        }

        public IDictionary<string, string> GetLocalizationMap(string languageEncoding)
        {
            // Create map
            var localizationMap = new Dictionary<string, string>();
            
            // Get file lines
            var lines = _allLocalizationMapText.Split(_lineSeperator);

            // Find language map from language encoding
            var languageEncodings = lines[0].Split(_fieldSeperator);
            var languageIndex = FindKeyIndex(languageEncodings, languageEncoding);

            // Parse into dictionary
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = line.Trim('\r').Split(_fieldSeperator);
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

        public void Add(string key, string value)
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

        public void Remove(string key)
        {
            var lines = _allLocalizationMapText.Split(_lineSeperator);
            var keys = new List<string>(lines.Length);
            
            foreach (var line in lines)
            {
                keys.Add(line.Split(_fieldSeperator)[0]);
            }

            var index = FindKeyIndex(keys, key);
            if (index > -1)
            {
                var path = GetLocalizationMapPath(key);
                var fullPath = $"{CsvDirectory}{path}";
                
                var newLines = lines.Where(str => str != lines[index]);
                var replaced = string.Join(_lineSeperator.ToString(), newLines);
                File.WriteAllText(fullPath, replaced);
            }
        }

        public void Edit(string key, string value)
        {
            Remove(key);
            Add(key, value);
        }

        private static string GetLocalizationMapPath(string key)
        {
            var path = string.Empty;
            foreach (var fileName in _fileNames)
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