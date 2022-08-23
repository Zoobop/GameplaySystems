using UnityEngine;

namespace Entity.InventorySystem
{
    public class PortableInventory : Inventory
    {
        [Header("Portability Details")] [SerializeField]
        protected Transform _bagHolder;

        [SerializeField] protected Bag _bag;
        [SerializeField] protected float _weightLimit;

        private GameObject _bagObject;

        protected override void Awake()
        {
            // Assign if null
            _bagObject = Instantiate(_bag.Prefab, _bagHolder);
            Name = _bag.Name;

            // Parent implementation
            base.Awake();
        }
    }
}