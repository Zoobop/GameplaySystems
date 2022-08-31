using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    using Entity.InventorySystem;
    using Utility.ExtensionMethods;
    
    public class InventoryPanel : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI _inventoryTitleText;

        [SerializeField] private Transform _contentHolder;
        [SerializeField] private EnhancedButton _moveButton;
        [SerializeField] private EnhancedButton _moveAllButton;

        [Header("UI Prefabs")] [SerializeField]
        private GameObject _inventorySlotPrefab;

        private readonly List<InventorySlotUI> _inventorySlots = new();

        #region UnityEvents

        private void Awake()
        {
            _moveButton.Disable();
        }

        #endregion

        public void OnInventoryChangedCallback(IInventory inventory)
        {
            // Update UI
            _inventoryTitleText.text = inventory.Name;

            // Get items
            var items = inventory.GetItemsAsList();
            GenerateSlots(items);

            if (_inventorySlots.IsEmpty())
            {
                _moveAllButton.Disable();
            }
            else
            {
                _moveAllButton.Enable();
            }
        }

        private void GenerateSlots(IEnumerable<ItemSlot> items)
        {
            // First time setup
            if (_inventorySlots.IsEmpty())
            {
                // Instantiate slots
                foreach (var itemSlot in items)
                {
                    var slot = Instantiate(_inventorySlotPrefab, _contentHolder).GetComponent<InventorySlotUI>();
                    slot.SetSlotValues(InventoryContentWindow.Instance, itemSlot);
                    _inventorySlots.Add(slot);
                }

                return;
            }

            // Get difference in item counts
            var itemCount = items.Count();
            var slotCount = _inventorySlots.Count;
            var countDifference = slotCount - itemCount;
            var count = Mathf.Abs(countDifference);

            // Item gain
            if (countDifference < 0)
            {
                // Update existing items
                for (var i = 0; i < slotCount; i++)
                {
                    //_inventorySlots[i].gameObject.SetActive(true);
                    _inventorySlots[i].SetSlotValues(InventoryContentWindow.Instance, items.ElementAt(i));
                }

                // Add new items
                for (var i = 0; i < count; i++)
                {
                    var slot = Instantiate(_inventorySlotPrefab, _contentHolder).GetComponent<InventorySlotUI>();
                    slot.SetSlotValues(InventoryContentWindow.Instance, items.ElementAt(slotCount + i));

                    _inventorySlots.Add(slot);
                }
            }
            // Item loss
            else if (countDifference > 0)
            {
                // Update existing items
                for (var i = 0; i < itemCount; i++)
                {
                    _inventorySlots[i].SetSlotValues(InventoryContentWindow.Instance, items.ElementAt(i));
                }

                // Remove empty/invalid slots
                for (var i = 0; i < count; i++)
                {
                    //_inventorySlots[i].gameObject.SetActive(false);
                    Destroy(_inventorySlots[itemCount].gameObject);
                    _inventorySlots.RemoveAt(itemCount);
                }
            }
            // Item shift or unchanged
            else
            {
                // Update existing items
                for (var i = 0; i < slotCount; i++)
                {
                    _inventorySlots[i].SetSlotValues(InventoryContentWindow.Instance, items.ElementAt(i));
                }
            }
        }

        public Transform GetContentHolder()
        {
            return _contentHolder;
        }

        public void SetMoveAllEvent(UnityAction action)
        {
            _moveAllButton.AddListener(action);
        }

        public void SetMoveEvent(UnityAction action)
        {
            _moveButton.AddListener(action);
        }

        public void EnableMove()
        {
            _moveButton.Enable();
        }

        public void DisableMove()
        {
            _moveButton.Disable();
        }
    }
}
