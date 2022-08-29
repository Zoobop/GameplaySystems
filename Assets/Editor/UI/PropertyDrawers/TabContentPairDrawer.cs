using UnityEditor;
using UnityEngine;

using UI;

namespace Editor.UI
{
    
    [CustomPropertyDrawer(typeof(TabContentPair))]
    public class TabContentPairDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var button = property.FindPropertyRelative("button");
            var tab = property.FindPropertyRelative("tab");

            var tabObject = (ITab) tab.objectReferenceValue;
            var tabName = tab.objectReferenceValue ? tabObject.GetTabName().Value : "Empty Tab";
            
            // Position calculate
            const float labelOffset = 50f;
            var labelPosition = new Rect(position.x, position.y, position.width - labelOffset, position.height);

            position = EditorGUI.PrefixLabel(labelPosition, GUIUtility.GetControlID(FocusType.Passive),
                new GUIContent(tabName));

            // Layout
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var widthSize = position.width / 2;

            var buttonPropPos = new Rect(position.x - 40, position.y, widthSize + labelOffset + 40, position.height);
            var tabPropPos = new Rect(position.x + widthSize + labelOffset + 10, position.y, widthSize,
                position.height);

            EditorGUI.PropertyField(buttonPropPos, button, GUIContent.none);
            EditorGUI.PropertyField(tabPropPos, tab, GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}