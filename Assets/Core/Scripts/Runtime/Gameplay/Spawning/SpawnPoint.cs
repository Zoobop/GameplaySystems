using UnityEngine;

namespace SpawnSystem
{
    using Utility.ExtensionMethods;

    public class SpawnPoint : MonoBehaviour, ISpawner
    {
        [Header("References")] [SerializeField]
        private GameObject _objectToSpawn;

        [Header("Gizmo")] [SerializeField] private Color _gizmoColor;
        [SerializeField] private Vector3 _gizmoOffset;
        [SerializeField] private Vector3 _gizmoSize;

        private GameObject _spawnedObject;

        #region UnityEvents

        private void Start()
        {
            Spawn();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(transform.position + _gizmoOffset, _gizmoSize);
        }

        #endregion

        #region ISpawner

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetObjectToSpawn(GameObject gameObj)
        {
            _objectToSpawn = gameObj;
        }

        public GameObject Spawn()
        {
            // Return if null
            if (!_objectToSpawn) return null;

            // Instantiate object
            _spawnedObject = Instantiate(_objectToSpawn, transform);
            _spawnedObject.transform.SetLocalPosition(Vector3.zero);
            return _spawnedObject;
        }

        #endregion
    }
}
