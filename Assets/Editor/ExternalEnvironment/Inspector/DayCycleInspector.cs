using UnityEditor;
using UnityEngine;

using ExternalEnvironment;
using Utility.ExtensionMethods;

namespace Editor.ExternalEnvironment
{
    using Utility;
   
    [CustomEditor(typeof(DayCycle))]
    public class DayCycleInspector : UnityEditor.Editor
    {
        /* Sun (Directional Light) */
        private SerializedProperty sunProperty;
        
        /* Time */
        private SerializedProperty dayProperty;
        private SerializedProperty timeProperty;
        private SerializedProperty timeRegionProperty;
        
        /* Cycle Details */
        private SerializedProperty tickSpeedProperty;

        private const float MaxTime = DayCycle.MaxTime;
        private const float MinTime = DayCycle.MinTime;
        
        private void OnEnable()
        {
            sunProperty = serializedObject.FindProperty("_sun");

            dayProperty = serializedObject.FindProperty("_day");
            timeProperty = serializedObject.FindProperty("_time");
            timeRegionProperty = serializedObject.FindProperty("_timeRegion");
            
            tickSpeedProperty = serializedObject.FindProperty("_tickSpeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            DrawSunArea();
            DrawTimeArea();
            DrawCycleDetailsArea();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSunArea()
        {
            InspectorUtility.DrawHeader("Sun (Directional Light)");
            
            var sun = ((DayCycle) target).GetComponentInChildren<Light>();
            if (sun)
            {
                sunProperty.objectReferenceValue = sun;
            }
            
            InspectorUtility.DrawDisabledFields(DrawSunDisabled);

            if (!sun)
            {
                StopDrawing("No Light source found!");
            }

            InspectorUtility.DrawSpace();
        }

        private void DrawSunDisabled()
        {
            sunProperty.DrawPropertyField();
        }

        private void DrawTimeArea()
        {
            InspectorUtility.DrawHeader($"Time ({(int)MinTime} - {(int)MaxTime})");

            var day = dayProperty.intValue;
            dayProperty.intValue = Mathf.Max(day, 1);

            dayProperty.DrawPropertyField();
            
            var time = timeProperty.floatValue;
            timeProperty.floatValue = Mathf.Clamp(time, MinTime, MaxTime);
            
            UpdateSunDirection();
            timeProperty.DrawPropertyField();

            timeRegionProperty.enumValueIndex = (int) DayCycle.GetTimeRegion(timeProperty.floatValue);

            InspectorUtility.DrawDisabledFields(DrawTimeRegionDisabled);

            InspectorUtility.DrawSpace();
        }

        private void UpdateSunDirection()
        {
            var sun = ((DayCycle) target).GetComponentInChildren<Light>();
            var startEulerAngles = new Vector3(0f, sun.transform.rotation.eulerAngles.y, 0f);
            var endEulerAngles = new Vector3(359.999f, startEulerAngles.y, 0f);
            
            var elapsedTime = timeProperty.floatValue / MaxTime;
            sun.transform.SetRotation(Vector3.Lerp(startEulerAngles, endEulerAngles, elapsedTime));
        }

        private void DrawTimeRegionDisabled()
        {
            timeRegionProperty.DrawPropertyField();
        }
        
        private void DrawCycleDetailsArea()
        {
            InspectorUtility.DrawHeader("Cycle Details");
            
            tickSpeedProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }
        
        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            InspectorUtility.DrawHelpBox(reason, messageType);

            InspectorUtility.DrawSpace();

            InspectorUtility.DrawHelpBox("You need a Light as a child of the GameObject to work properly at Runtime!", MessageType.Warning);

            serializedObject.ApplyModifiedProperties();
        }
    }
}