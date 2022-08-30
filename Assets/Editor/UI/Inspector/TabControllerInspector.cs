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

        private void OnEnable()
        {
            tabsProperty = serializedObject.FindProperty("_tabs");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawTabsArea();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTabsArea()
        {
            InspectorUtility.DrawHeader("Tab Content Pairs");

            tabsProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }
    }
}