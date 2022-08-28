using UnityEditor;
using UnityEngine;

using LocalizationSystem;
using Local = LocalizationSystem.LocalizationSystem;

namespace Editor.LocalizationSystem
{
    [CustomPropertyDrawer(typeof(LocalizedString))]
    public class LocalizedStringPropertyDrawer : PropertyDrawer
    {
        private const float DefaultHeight = 20f;
        private const float HeightModifier = 25f;
        
        private bool dropdown;
        private float height;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return dropdown ? height + HeightModifier : DefaultHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.width -= 34;
            position.height = 18;

            var valueRect = new Rect(position);
            valueRect.x += 15;
            valueRect.width -= 15;

            var foldButtonRect = new Rect(position)
            {
                width = 15
            };

            dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

            position.x += 15;
            position.width -= 15;

            var key = property.FindPropertyRelative("_key");
            key.stringValue = EditorGUI.TextField(position, key.stringValue);

            position.x += position.width + 2;
            position.width = 17;
            position.height = 17;
            
            var searchContent = new GUIContent("0");

            if (GUI.Button(position, searchContent))
            {
                LocalizationSystemSearchWindow.Open();
            }

            position.x += position.width + 2;
            
            var storeContent = new GUIContent("$");

            if (GUI.Button(position, storeContent))
            {
                LocalizationSystemModificationWindow.Open(key.stringValue);
            }

            if (dropdown)
            {
                var value = Local.GetLocalizedValue(key.stringValue);
                var style = GUI.skin.box;
                height = style.CalcHeight(new GUIContent(value), valueRect.width);

                valueRect.height = height;
                valueRect.y += 21;
                EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
            }
            
            EditorGUI.EndProperty();
        }
    }
}