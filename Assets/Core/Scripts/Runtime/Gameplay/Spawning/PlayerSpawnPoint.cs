using Entity;
using UnityEngine;

namespace SpawnSystem
{
    public class PlayerSpawnPoint : MonoBehaviour, ISpawner
    {
        [Header("Gizmo")] [SerializeField] private Vector3 _gizmoOffset;
        [SerializeField] private Vector3 _gizmoSize;

        private GameObject _player;

        #region UnityEvents

        private void Start()
        {
            Spawn();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
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
        }

        [ContextMenu("Spawn")]
        public GameObject Spawn()
        {
            // Instantiate object
            var spawnPointTransform = transform;
            _player = Instantiate(GameManager.Player.gameObject, spawnPointTransform);

            // Set as current player
            GameManager.SetCurrentPlayer(_player.GetComponent<ICharacter>());
            return _player;
        }

        #endregion
    }
}
