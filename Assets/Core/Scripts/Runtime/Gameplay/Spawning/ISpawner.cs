

using UnityEngine;

namespace SpawnSystem
{
    public interface ISpawner
    {
        public Transform GetTransform();
        public void SetObjectToSpawn(GameObject gameObj);
        public GameObject Spawn();
    }
}