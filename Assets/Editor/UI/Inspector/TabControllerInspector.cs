using System.Collections.Generic;
using UnityEditor;

using UI;

namespace Editor.UI
{
    using Utility;
    
    [CustomEditor(typeof(TabController))]
    public class TabControllerInspector : UnityEditor.Editor
    {
        /* Tabs */
        private SerializedProperty tabsProperty;
        private SerializedProperty activeTabProperty;

        private void OnEnable()
        {
            tabsProperty = serializedObject.FindProperty("_tabs");
            activeTabProperty = serializedObject.FindProperty("_activeTab");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var size = tabsProperty.arraySize;
            if (size > 0)
            {
                ((ITab)tabsProperty.GetArrayElementAtIndex(0)
                    .FindPropertyRelative("tab")
                    .objectReferenceValue)?.SetActive();
            }
            
            DrawTabsArea();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTabsArea()
        {
            InspectorUtility.DrawHeader("Tab Content Pairs");

            tabsProperty.DrawPropertyField();
            InspectorUtility.DrawDisabledFields(DrawActiveTabDisabled);
            
            InspectorUtility.DrawSpace();
        }

        private void DrawActiveTabDisabled()
        {
            activeTabProperty.DrawPropertyField();
        }
    }
}