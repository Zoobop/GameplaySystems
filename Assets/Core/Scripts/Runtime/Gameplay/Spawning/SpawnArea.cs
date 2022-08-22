using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnSystem
{
    using Utility.ExtensionMethods;
    
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnPointPrefab;
        [SerializeField] private List<GameObject> _entitiesToSpawn = new();

        [SerializeField] private bool _randomizeSpawns = false;

        [SerializeField] private LayerMask _groundMask;
        [SerializeField, Min(0)] private float _rayToGroundDistance = 1000f;

        [SerializeField, Min(1)] private int _spawnAmount = 1;
        [SerializeField, Min(0)] private float _spawnRadius = 5f;
        [SerializeField, Min(0)] private float _minimumDistanceApart = 2f;
        
        private readonly ICollection<Vector3> _positions = new List<Vector3>();
        private readonly ICollection<ISpawner> _spawners = new List<ISpawner>();

        private Vector3 RandomPoint
        {
            get
            {
                var point = Random.insideUnitCircle * _spawnRadius;
                return new Vector3(point.x, 0, point.y);
            }
        }

        private GameObject RandomEntity
        {
            get
            {
                var entity = _entitiesToSpawn.IsEmpty()
                    ? null
                    : _entitiesToSpawn[Random.Range(0, _entitiesToSpawn.Count)];
                return entity;
            }
        }

        #region UnityEvents

        private void OnDrawGizmosSelected()
        {
            if (!_randomizeSpawns) return;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }

        #endregion

        #region Helpers

        public void GenerateSpawners()
        {
            ClearSpawners();

            // Instantiate spawners
            var count = _randomizeSpawns ? _spawnAmount : _entitiesToSpawn.Count;
            for (var i = 0; i < count; i++)
            {
                // Instantiate spawner object
                var spawnerObj = Instantiate(_spawnPointPrefab, transform);
                SetSpawnerPosition(spawnerObj.transform);

                // Set spawner values
                var spawner = spawnerObj.GetComponent<ISpawner>();
                var entity = _randomizeSpawns ? RandomEntity : _entitiesToSpawn[i];
                spawner.SetObjectToSpawn(entity);

                // Add to list
                _spawners.Add(spawner);
            }
        }

        private void ClearSpawners()
        {
            var children = GetComponentsInChildren<ISpawner>();
            foreach (var child in children)
            {
                DestroyImmediate(child.GetTransform().gameObject);
            }

            _spawners.Clear();
        }

        private void SetSpawnerPosition(in Transform spawner)
        {
            // Get y position of ground level
            var randomPoint = FindValidPosition();
            if (Physics.Raycast(randomPoint, Vector3.down, out var hit, _rayToGroundDistance, _groundMask))
            {
                var distance = Vector3.down * hit.distance;
                var newPosition = randomPoint + distance;
                spawner.SetLocalPosition(newPosition);
            }
        }

        private Vector3 FindValidPosition()
        {
            // Get potential spawn point
            var spawnPoint = RandomPoint;

            // Loops until a valid spawn point is found
            while (true)
            {
                if (CheckPosition(spawnPoint))
                {
                    _positions.Add(spawnPoint);
                    break;
                }

                // Get another random point
                spawnPoint = RandomPoint;
            }

            return spawnPoint;
        }

        private bool CheckPosition(Vector3 position)
        {
            // Compare each distance
            foreach (var spawnerPos in _positions)
            {
                if (position.Distance(spawnerPos) < _minimumDistanceApart)
                {
                    return false;
                }
            }
            
            // If reached, meets distance requirement
            return true;
        }

        #endregion
    }
}