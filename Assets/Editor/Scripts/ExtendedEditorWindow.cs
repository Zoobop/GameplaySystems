using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject _serializedObject;
    protected SerializedProperty _currentProperty;

    protected void DrawProperties(SerializedProperty property, bool drawChildren)
    {
        var lastPropertyPath = string.Empty;
        foreach (SerializedProperty p in property)
        {
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lastPropertyPath) && p.propertyPath.Contains(lastPropertyPath)) { continue; }
                lastPropertyPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, drawChildren);
            }
        }
    }

}
