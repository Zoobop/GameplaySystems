using System;
using UnityEngine;

namespace Entity.InventorySystem
{
    [Serializable]
    public class ItemSlot
    {
        [Tooltip("The current item.")] [SerializeField]
        private Item _item;

        [Tooltip("The current amount of this item.")] [SerializeField]
        private int _amount;

        [Tooltip("The max stack of this item.")] [SerializeField]
        private int _maxStack;

        public Item Item
        {
            get => _item;
            set => _item = value;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public ItemSlot(Item item, int amount)
        {
            _item = item;
            _amount = amount;
            _maxStack = item ? item.MaxStack : 0;
        }

        public void Deconstruct(out Item item, out int amount)
        {
            item = _item;
            amount = _amount;
        }

        public override string ToString()
        {
            return $"{_item.Name} x{_amount}";
        }
    }
}