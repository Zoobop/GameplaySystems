using SpawnSystem;
using UnityEditor;

namespace Editor.SpawnSystem
{
    using Utility;
    
    [CustomEditor(typeof(SpawnArea))]
    public class SpawnAreaInspector : UnityEditor.Editor
    {
        /* General Spawner Details */
        private SerializedProperty spawnerPrefabProperty;
        private SerializedProperty entitiesToSpawnProperty;

        /* Filters */
        private SerializedProperty randomizeSpawnsProperty;
        
        /* Randomized Spawn Values */
        private SerializedProperty groundMaskProperty;
        private SerializedProperty rayToGroundDistanceProperty;
        
        private SerializedProperty spawnAmountProperty;
        private SerializedProperty spawnRadiusProperty;
        private SerializedProperty minimumDistanceApartProperty;

        private void OnEnable()
        {
            spawnerPrefabProperty = serializedObject.FindProperty("_spawnPointPrefab");
            entitiesToSpawnProperty = serializedObject.FindProperty("_entitiesToSpawn");
            
            randomizeSpawnsProperty = serializedObject.FindProperty("_randomizeSpawns");

            groundMaskProperty = serializedObject.FindProperty("_groundMask");
            rayToGroundDistanceProperty = serializedObject.FindProperty("_rayToGroundDistance");
            
            spawnAmountProperty = serializedObject.FindProperty("_spawnAmount");
            spawnRadiusProperty = serializedObject.FindProperty("_spawnRadius");
            minimumDistanceApartProperty = serializedObject.FindProperty("_minimumDistanceApart");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawSpawnerPrefabArea();
            
            var prefab = spawnerPrefabProperty.objectReferenceValue;
            if (prefab is not null)
            {
                DrawSpawnPoolArea();
                DrawFilterArea();

                var isRandomized = randomizeSpawnsProperty.boolValue;
                if (isRandomized)
                {
                    DrawRandomizeDetailsArea();
                }
                
                DrawSpawnerSetupButton("Setup Spawners");
                
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                StopDrawing("Spawner Prefab has not been assigned!");
            }
        }

        private void DrawSpawnerPrefabArea()
        {
            InspectorUtility.DrawHeader("Spawner Prefab");

            spawnerPrefabProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }
        
        private void DrawSpawnPoolArea()
        {
            InspectorUtility.DrawHeader("Spawn Pool");

            entitiesToSpawnProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }

        private void DrawFilterArea()
        {
            InspectorUtility.DrawHeader("Filters");

            randomizeSpawnsProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }

        private void DrawRandomizeDetailsArea()
        {
            InspectorUtility.DrawHeader("Randomization Details");

            groundMaskProperty.DrawPropertyField();
            rayToGroundDistanceProperty.DrawPropertyField();
            
            InspectorUtility.DrawSpace(2);
            
            spawnAmountProperty.DrawPropertyField();
            spawnRadiusProperty.DrawPropertyField();
            minimumDistanceApartProperty.DrawPropertyField();

            InspectorUtility.DrawSpace();
        }
        
        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            InspectorUtility.DrawHelpBox(reason, messageType);

            InspectorUtility.DrawSpace();

            InspectorUtility.DrawHelpBox("You need to select a Spawner prefab for this component to work properly at Runtime!", MessageType.Warning);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSpawnerSetupButton(string text)
        {
            var spawnAreaReference = (SpawnArea) target;
            
            InspectorUtility.DrawButton(text, spawnAreaReference.GenerateSpawners);
            
            InspectorUtility.DrawSpace();
        }
    }
}