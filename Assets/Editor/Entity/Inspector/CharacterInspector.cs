using System;
using UnityEditor;

using Entity;
using Local = Core.LocalizationSystem.LocalizationSystem;

namespace Editor.Entity
{
    using Utility;
    
    [CustomEditor(typeof(Character), true)]
    public class CharacterInspector : UnityEditor.Editor
    {
        /* GameObject Details */
        private SerializedProperty modelProperty;
        
        /* References */
        private SerializedProperty statsProperty;
        private SerializedProperty levelProgressionProperty;
        private SerializedProperty inventoryProperty;
        private SerializedProperty skillBagProperty;
        private SerializedProperty characterMovementProperty;
        private SerializedProperty interactionHandlerProperty;
        private SerializedProperty dialogueArchiveProperty;
        
        /* Character Details */
        private SerializedProperty titleProperty;
        private SerializedProperty nameProperty;
        private SerializedProperty fullNameProperty;
        private SerializedProperty fullNameFormatIndexProperty;

        private readonly string[] _fullNameFormats =
        {
            "{0}",
            "{1} {0}",
            "{0} of {1}",
            "{0}, the {1}"
        };

        private readonly string[] _formatReps =
        {
            "(Name)",
            "(Title) (Name)",
            "(Name) of (Title)",
            "(Name), the (Title)"
        };

        private void OnEnable()
        {
            modelProperty = serializedObject.FindProperty("_model");

            statsProperty = serializedObject.FindProperty("_stats");
            levelProgressionProperty = serializedObject.FindProperty("_levelProgression");
            inventoryProperty = serializedObject.FindProperty("_inventory");
            skillBagProperty = serializedObject.FindProperty("_skillBag");
            characterMovementProperty = serializedObject.FindProperty("_movement");
            interactionHandlerProperty = serializedObject.FindProperty("_interactionHandler");
            dialogueArchiveProperty = serializedObject.FindProperty("_dialogueArchive");
            
            titleProperty = serializedObject.FindProperty("_title");
            nameProperty = serializedObject.FindProperty("_name");
            fullNameProperty = serializedObject.FindProperty("_fullName");
            fullNameFormatIndexProperty = serializedObject.FindProperty("_fullNameFormatIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            DrawGameObjectDetailsArea();
            DrawReferences();
            DrawCharacterDetails();
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void DrawGameObjectDetailsArea()
        {
            InspectorUtility.DrawHeader("GameObject Details");
            
            InspectorUtility.DrawDisabledFields(DrawGameObjectDetails);

            if (modelProperty.objectReferenceValue == null)
            {
                StopDrawing("Model object cannot be found!");
            }
            
            InspectorUtility.DrawSpace();
        }

        private void DrawGameObjectDetails()
        {
            // Try to get and assign model object
            var transform = ((Character) target).transform;
            var model = transform.Find("Model");

            modelProperty.objectReferenceValue = model.gameObject;
            modelProperty.DrawPropertyField();
        }

        private void DrawReferences()
        {
            InspectorUtility.DrawHeader("References");
            
            statsProperty.DrawPropertyField();
            levelProgressionProperty.DrawPropertyField();
            inventoryProperty.DrawPropertyField();
            skillBagProperty.DrawPropertyField();
            characterMovementProperty.DrawPropertyField();
            interactionHandlerProperty.DrawPropertyField();
            dialogueArchiveProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }
        
        private void DrawCharacterDetails()
        {
            InspectorUtility.DrawHeader("Character Details");

            fullNameFormatIndexProperty.intValue = InspectorUtility.DrawToolbar(_formatReps, fullNameFormatIndexProperty.intValue);
            var title = Local.GetLocalizedValue(titleProperty.FindPropertyRelative("_key").stringValue);
            var name = Local.GetLocalizedValue(nameProperty.FindPropertyRelative("_key").stringValue);

            if (fullNameFormatIndexProperty.intValue != 0)
            {
                titleProperty.DrawPropertyField();
            }
            
            nameProperty.DrawPropertyField();
            fullNameProperty.stringValue = string.Format(_fullNameFormats[fullNameFormatIndexProperty.intValue], name, title);
            
            InspectorUtility.DrawDisabledFields(DrawFullNameDisabled);

            InspectorUtility.DrawSpace();
        }

        private void DrawFullNameDisabled()
        {
            fullNameProperty.DrawPropertyField();
        }
        
        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            InspectorUtility.DrawHelpBox(reason, messageType);

            InspectorUtility.DrawSpace();

            InspectorUtility.DrawHelpBox("You need a child GameObject called \'Model\' to work properly at Runtime!", MessageType.Warning);

            serializedObject.ApplyModifiedProperties();
        }
    }
}