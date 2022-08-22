using UnityEngine;

namespace InteractionSystem
{

    using Entity;

    public interface IOpenable : IEntity
    {
        public void Toggle(Vector3 position = new());
        public void Open(Vector3 position = new());
        public void Close();
    }
}