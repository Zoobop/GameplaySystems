using UnityEngine;

namespace Entity.InventorySystem
{
    [CreateAssetMenu(menuName = "Items/Bag", fileName = "Bag_")]
    public class Bag : Item
    {
        [Header("Bag Details")] [Min(0)] [SerializeField]
        private float _weightLimit = 30f;

        public float WeightLimit => _weightLimit;
    }
}