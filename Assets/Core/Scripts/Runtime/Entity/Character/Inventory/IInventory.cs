using System;
using System.Collections.Generic;

namespace Entity.InventorySystem
{
    public interface IInventory
    {
        public string Name { get; set; }
        public event Action<IInventory> OnInventoryChanged;
        public bool AddItem(Item itemToAdd, int amount = 1);
        public bool AddItems(IEnumerable<ItemSlot> items);
        public bool AddItems(IInventory inventory);
        public bool RemoveItem(Item itemToRemove, int amount = 1);
        public bool RemoveItems(IEnumerable<ItemSlot> items);
        public bool RemoveItems(IInventory inventory);
        public void Discard(ItemSlot slot, int amount);
        public bool HasItem(Item item, int amount = 1);
        public bool HasItemSlot(ItemSlot itemSlot);
        public ItemSlot Transfer(Item item);
        public IEnumerable<ItemSlot> TransferAll();
        public IList<ItemSlot> GetItemsAsList();
        public IDictionary<Item, int> GetItemsAsDictionary();
    }
}
