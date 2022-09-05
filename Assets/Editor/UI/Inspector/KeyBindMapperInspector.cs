﻿using UnityEditor;

using UI;
using InputSystem;

namespace Editor.UI
{
    using Utility;
    
    [CustomEditor(typeof(KeyBindMapper))]
    public class KeyBindMapperInspector : UnityEditor.Editor
    {
        /* Input */
        private SerializedProperty inputTypeProperty;
        
        /* Movement */
        private SerializedProperty moveForwardProperty;
        private SerializedProperty moveBackProperty;
        private SerializedProperty moveLeftProperty;
        private SerializedProperty moveRightProperty;
        private SerializedProperty movementProperty;
        private SerializedProperty sprintProperty;
        
        /* Interaction */
        private SerializedProperty interactProperty;

        /* UI */
        private SerializedProperty pauseMenuProperty;
        private SerializedProperty tabLeftProperty;
        private SerializedProperty tabRightProperty;

        private void OnEnable()
        {
            inputTypeProperty = serializedObject.FindProperty("_inputType");

            moveForwardProperty = serializedObject.FindProperty("_moveForward");
            moveBackProperty = serializedObject.FindProperty("_moveBack");
            moveLeftProperty = serializedObject.FindProperty("_moveLeft");
            moveRightProperty = serializedObject.FindProperty("_moveRight");
            movementProperty = serializedObject.FindProperty("_movement");
            sprintProperty = serializedObject.FindProperty("_sprint");
            
            interactProperty = serializedObject.FindProperty("_interact");

            pauseMenuProperty = serializedObject.FindProperty("_pauseMenu");
            tabLeftProperty = serializedObject.FindProperty("_tabLeft");
            tabRightProperty = serializedObject.FindProperty("_tabRight");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawInputTypeArea();

            var type = (InputType)inputTypeProperty.enumValueIndex;
            if (type == InputType.Keyboard)
            {
                DrawKeyboardMovementKeyBindsArea();
            }
            else
            {
                DrawGamepadMovementKeyBindArea();
            }

            sprintProperty.DrawPropertyField();
            InspectorUtility.DrawSpace();
            
            DrawInteractionKeyBindArea();
            DrawUIKeyBinds();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawInputTypeArea()
        {
            InspectorUtility.DrawHeader("General");

            inputTypeProperty.DrawPropertyField();
            
            InspectorUtility.DrawSpace();
        }

        private void DrawKeyboardMovementKeyBindsArea()
        {
            InspectorUtility.DrawHeader("Keyboard Movement Binds");

            moveForwardProperty.DrawPropertyField();
            moveBackProperty.DrawPropertyField();
            moveLeftProperty.DrawPropertyField();
            moveRightProperty.DrawPropertyField();
        }
        
        private void DrawGamepadMovementKeyBindArea()
        {
            InspectorUtility.DrawHeader("Gamepad Movement Binds");

            movementProperty.DrawPropertyField();
        }

        private void DrawInteractionKeyBindArea()
        {
            InspectorUtility.DrawHeader("Interaction Binds");

            interactProperty.DrawPropertyField();
            
            InspectorUtility.DrawSpace();
        }
        
        private void DrawUIKeyBinds()
        {
            InspectorUtility.DrawHeader("UI Binds");

            pauseMenuProperty.DrawPropertyField();
            tabLeftProperty.DrawPropertyField();
            tabRightProperty.DrawPropertyField();
            
            InspectorUtility.DrawSpace();
        }
    }
}