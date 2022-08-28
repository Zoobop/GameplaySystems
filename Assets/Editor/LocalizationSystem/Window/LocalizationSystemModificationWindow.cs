using UnityEditor;
using UnityEngine;

using Local = LocalizationSystem.LocalizationSystem;

namespace Editor.LocalizationSystem
{
    public class LocalizationSystemModificationWindow : EditorWindow
    {
        private string key;
        private string value;
        
        public static void Open(string key)
        {
            var window = CreateInstance<LocalizationSystemModificationWindow>();
            window.titleContent = new GUIContent("Localization Window");
            window.ShowUtility();
            window.key = key;
        }

        public void OnGUI()
        {
            key = EditorGUILayout.TextField("Key :", key);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", GUILayout.MaxWidth(50));

            EditorStyles.textArea.wordWrap = true;
            value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                if (Local.GetLocalizedValue(key) != string.Empty)
                {
                    Local.Replace(key, value);
                }
                else
                {
                    Local.Add(key, value);
                }
            }

            minSize = new Vector2(460, 250);
            maxSize = minSize;
        }
    }
}