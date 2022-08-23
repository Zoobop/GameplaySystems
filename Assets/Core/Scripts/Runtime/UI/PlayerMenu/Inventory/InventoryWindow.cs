using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    using Entity;
    using Entity.InventorySystem;
    using Entity.Item;
    using Utility.ExtensionMethods;
    
    public class InventoryWindow : MonoBehaviour, IWindow
    {
        public enum ItemCategory
        {
            All = 0,
            Consumable,
            Armament,
            KeyItem,
            Misc,
        }

        [Header("References")] [SerializeField]
        private Canvas _canvas;

        [SerializeField] private Transform _contentHolder;
        [SerializeField] private GameObject _inventorySlotPrefab;
        [SerializeField] private ItemDetailsPanel _itemDetailsPanel;

        private IInventory _inventory;
        private ItemCategory _itemCategory = ItemCategory.All;
        private List<InventorySlotUI> _itemSlots = new();
        private InventorySlotUI _currentSelected;
        private bool _isOpen;

        public bool IsWindowActive => _canvas.enabled;

        #region UnityEvent



        #endregion

        #region WindowUtility

        public void Bind(IInventory inventory)
        {
            // Assign
            _inventory = inventory;

            // Get items
            var items = inventory.GetItemsAsList();

            // Get slots
            GenerateSlots(items);
        }

        private void GenerateSlots(IEnumerable<ItemSlot> items)
        {
            // First time setup
            if (_itemSlots.IsEmpty())
            {
                // Instantiate slots
                foreach (var itemSlot in items)
                {
                    var slot = Instantiate(_inventorySlotPrefab, _contentHolder).GetComponent<InventorySlotUI>();
                    slot.SetSlotValues(this, itemSlot);
                    _itemSlots.Add(slot);
                }

                return;
            }

            // Get difference in item counts
            var itemCount = items.Count();
            var slotCount = _itemSlots.Count;
            var countDifference = slotCount - itemCount;
            var count = Mathf.Abs(countDifference);

            // Item gain
            if (countDifference < 0)
            {
                // Update existing items
                for (var i = 0; i < slotCount; i++)
                {
                    //_inventorySlots[i].gameObject.SetActive(true);
                    _itemSlots[i].SetSlotValues(this, items.ElementAt(i));
                }

                // Add new items
                for (var i = 0; i < count; i++)
                {
                    var slot = Instantiate(_inventorySlotPrefab, _contentHolder).GetComponent<InventorySlotUI>();
                    slot.SetSlotValues(this, items.ElementAt(slotCount + i));
                    _itemSlots.Add(slot);
                }
            }
            // Item loss
            else if (countDifference > 0)
            {
                // Update existing items
                for (var i = 0; i < itemCount; i++)
                {
                    _itemSlots[i].SetSlotValues(this, items.ElementAt(i));
                }

                // Remove empty/invalid slots
                for (var i = 0; i < count; i++)
                {
                    //_inventorySlots[i].gameObject.SetActive(false);
                    Destroy(_itemSlots[itemCount].gameObject);
                    _itemSlots.RemoveAt(itemCount);
                }
            }
            // Item shift or unchanged
            else
            {
                // Update existing items
                for (var i = 0; i < slotCount; i++)
                {
                    _itemSlots[i].SetSlotValues(this, items.ElementAt(i));
                }
            }
        }

        public void OpenWindow()
        {
            _canvas.enabled = true;

            Deselect();
        }

        public void CloseWindow()
        {
            _canvas.enabled = false;

            Deselect();
        }

        private void UpdateSlots()
        {
            // Return if empty
            if (_itemSlots.IsEmpty()) return;

            // All category
            if (_itemCategory == ItemCategory.All)
            {
                foreach (var slot in _itemSlots)
                {
                    slot.gameObject.SetActive(true);
                }

                return;
            }

            // Enable slots with match item category
            foreach (var slot in _itemSlots)
            {
                if (slot.ItemCategory != _itemCategory)
                {
                    slot.gameObject.SetActive(false);
                    continue;
                }

                slot.gameObject.SetActive(true);
            }
        }

        public void SetCategory(int category)
        {
            // Set category
            _itemCategory = (ItemCategory) category;

            // Update
            UpdateSlots();
        }

        public void Discard()
        {
            var slot = _currentSelected.ItemSlot;
            var discardAmount = _itemDetailsPanel.GetDiscardAmount();
            print(discardAmount);

            // Discard item by amount
            _inventory.Discard(slot, discardAmount);

            // Deselect if item doesn't exist within inventory
            if (!_inventory.HasItem(slot.Item) || !_inventory.HasItemSlot(slot))
            {
                Deselect();
                return;
            }

            // Re-select to update amount dropdown
            Select(_currentSelected);
        }

        public void Use()
        {
            var slot = _currentSelected.ItemSlot;
            var useAmount = _itemDetailsPanel.GetUseAmount();
            print(useAmount);

            // Discard item by amount
            _inventory.Discard(slot, useAmount);
            // Use item
            ((IUsable) slot.Item).Use(useAmount);

            // Deselect if item doesn't exist within inventory
            if (!_inventory.HasItem(slot.Item) || !_inventory.HasItemSlot(slot))
            {
                Deselect();
                return;
            }

            // Re-select to update amount dropdown
            Select(_currentSelected);
        }

        #endregion

        #region Static

        public static ItemCategory GetCategory(Type type)
        {
            if (type == typeof(Consumable)) return ItemCategory.Consumable;
            if (type == typeof(Weapon) || type == typeof(Armor)) return ItemCategory.Armament;
            if (type == typeof(KeyItem)) return ItemCategory.KeyItem;
            return ItemCategory.Misc;
        }

        #endregion

        #region IWindow

        public void Select(IWindowElement element)
        {
            var slot = element as InventorySlotUI;
            _currentSelected = slot;
            _itemDetailsPanel.SetSelectedItemData(slot);
        }

        public void Deselect()
        {
            _currentSelected = null;
            _itemDetailsPanel.SetSelectedItemData(null);
        }

        public bool IsWindowOpen()
        {
            return _isOpen;
        }

        #endregion
    }
}
