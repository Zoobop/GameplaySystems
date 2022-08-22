using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using Local = Core.LocalizationSystem.LocalizationSystem;

namespace Editor.LocalizationSystem
{

    public class LocalizationSystemSearchWindow : EditorWindow
    {
        private string value = string.Empty;
        private Vector2 scroll;
        private IDictionary<string, string> localizedMap;

        public static void Open()
        {
            var window = CreateInstance<LocalizationSystemSearchWindow>();
            window.titleContent = new GUIContent("Localization Search");

            var mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            var rect = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
            window.ShowAsDropDown(rect, new Vector2(500, 300));
        }

        private void OnEnable()
        {
            localizedMap = Local.GetLocalizedMap();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
            value = EditorGUILayout.TextField(value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }

        private void GetSearchResults()
        {
            if (value == null) return;

            EditorGUILayout.BeginVertical();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (var (key, val) in localizedMap)
            {
                if (key.ToUpper().Contains(value.ToUpper()) || key.ToLower().Contains(value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("Box");

                    var content = new GUIContent("X");
                    if (GUILayout.Button(content, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                    {
                        if (EditorUtility.DisplayDialog($"Remove Key \'{key}\'?", "This will remove the element from localization database. Are you sure?", "Yes"))
                        {
                            Local.Remove(key);
                            AssetDatabase.Refresh();
                            Local.Init();
                            localizedMap = Local.GetLocalizedMap();
                        }
                    }

                    EditorGUILayout.TextField(key);
                    EditorGUILayout.LabelField(val);
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}