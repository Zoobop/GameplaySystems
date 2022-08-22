using UnityEngine;

namespace Entity
{
    public interface IEntity
    {
        public string GetName();
        public Transform GetTransform();
        public Transform Spawn(Transform parent = null);
        public void Despawn(bool destroy = true);
    }
}