using UnityEngine;

namespace InteractionSystem
{

    using Entity;

    public struct ItemPack
    {
        public Item item;
        public int amount;

        public ItemPack(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void Deconstruct(out Item item, out int amount)
        {
            item = this.item;
            amount = this.amount;
        }
    }

    public class Collectable : MonoBehaviour, ICollectable
    {
        [Header("Collectable Details")] [SerializeField]
        private Item _itemData;

        [SerializeField, Min(1)] private int _amount = 1;

        public string GetName()
        {
            return _itemData.Name;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Transform Spawn(Transform parent = null)
        {
            if (parent != null)
            {
                return Instantiate(transform);
            }

            return Instantiate(transform, parent);
        }

        public void Despawn(bool destroy = true)
        {
            if (destroy)
            {
                Destroy(gameObject);
                return;
            }

            gameObject.SetActive(false);
        }

        public ItemPack Collect()
        {
            return new ItemPack(_itemData, _amount);
        }
    }
}