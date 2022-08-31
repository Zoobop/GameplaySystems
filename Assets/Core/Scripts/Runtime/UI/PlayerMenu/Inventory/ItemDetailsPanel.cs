using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    using Entity;
    using Entity.InventorySystem;
    
    public class ItemDetailsPanel : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private TextMeshProUGUI _itemNameText;

        [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        [SerializeField] private EnhancedButton _discardButton;
        [SerializeField] private EnhancedButton _useButton;
        [SerializeField] private EnhancedDropdown _discardAmountDropdown;
        [SerializeField] private EnhancedDropdown _useAmountDropdown;

        private readonly string NamePlaceholder = "Select an item to view.";
        private readonly string DescriptionPlaceholder = "Selected item's description will appear here.";

        private readonly List<string> _allOptions = new()
        {
            "All",
            "1",
            "2",
            "3",
            "5",
            "10",
            "25",
            "50",
            "100",
            "200",
            "500",
        };

        private ItemSlot _itemSlot;

        #region UnityEvents



        #endregion

        public void SetSelectedItemData(InventorySlotUI slot)
        {
            // Check if current selected item is valid
            if (slot != null)
            {
                // Assign
                _itemSlot = slot.ItemSlot;

                // Get item
                var item = slot.ItemSlot.Item;

                // Set item name
                _itemNameText.text = item.Name;
                // Set item description
                _itemDescriptionText.text = $"{item.Description}\n\n{item.Lore}";

                // Update button usage
                if (item.IsDiscardable)
                {
                    _discardButton.Enable();

                    // Check if options have changed
                    var newOptions = GetOptions(slot.ItemSlot.Amount);
                    if (_discardAmountDropdown.HaveOptionsChanged(newOptions))
                    {
                        _discardAmountDropdown.SetOptions(newOptions);
                    }

                    _discardAmountDropdown.Enable();
                }
                else
                {
                    _discardButton.Disable();
                    _discardAmountDropdown.ResetPlaceholderText();
                    _discardAmountDropdown.Disable();
                }

                if (item is IUsable usableItem)
                {
                    _useButton.Enable();

                    // Check if options have changed
                    var newOptions = GetOptions(slot.ItemSlot.Amount);
                    if (_useAmountDropdown.HaveOptionsChanged(newOptions))
                    {
                        _useAmountDropdown.SetOptions(newOptions);
                    }

                    _useAmountDropdown.Enable();
                }
                else
                {
                    _useButton.Disable();
                    _useAmountDropdown.ResetPlaceholderText();
                    _useAmountDropdown.Disable();
                }

                return;
            }

            // Revert to default placeholders
            Default();
        }

        private void Default()
        {
            // Set text to be placeholders
            _itemNameText.text = NamePlaceholder;
            _itemDescriptionText.text = DescriptionPlaceholder;

            // Disable buttons
            _discardButton.Disable();
            _useButton.Disable();

            // Disable dropdowns
            _discardAmountDropdown.Disable();
            _useAmountDropdown.Disable();
            _discardAmountDropdown.ResetPlaceholderText();
            _useAmountDropdown.ResetPlaceholderText();
        }

        private List<string> GetOptions(int amount)
        {
            return amount switch
            {
                // Create options
                >= 500 => _allOptions,
                >= 200 => _allOptions.GetRange(0, 10),
                >= 100 => _allOptions.GetRange(0, 9),
                >= 50 => _allOptions.GetRange(0, 8),
                >= 25 => _allOptions.GetRange(0, 7),
                >= 10 => _allOptions.GetRange(0, 6),
                >= 5 => _allOptions.GetRange(0, 5),
                >= 3 => _allOptions.GetRange(0, 4),
                >= 2 => _allOptions.GetRange(0, 3),
                >= 1 => _allOptions.GetRange(0, 2),
                _ => _allOptions.GetRange(0, 1)
            };
        }

        public int GetDiscardAmount()
        {
            // Get amounts
            var option = _discardAmountDropdown.GetSelectedOption();
            var actualAmount = _itemSlot.Amount;

            // Calculate discard amount
            if (option == "All") return actualAmount;

            var optionAmount = int.Parse(option);
            return actualAmount - optionAmount >= 0 ? optionAmount : actualAmount;
        }

        public int GetUseAmount()
        {
            // Get amounts
            var option = _useAmountDropdown.GetSelectedOption();
            var actualAmount = _itemSlot.Amount;

            // Calculate use amount
            if (option == "All") return actualAmount;

            var optionAmount = int.Parse(option);
            return actualAmount - optionAmount >= 0 ? optionAmount : actualAmount;
        }

        public void AddDiscardEvent(UnityAction action)
        {
            _discardButton.AddListener(action);
        }

        public void RemoveDiscardEvent(UnityAction action)
        {
            _discardButton.RemoveListener(action);
        }

        public void AddUseEvent(UnityAction action)
        {
            _useButton.AddListener(action);
        }

        public void RemoveUseEvent(UnityAction action)
        {
            _useButton.RemoveListener(action);
        }
    }
}