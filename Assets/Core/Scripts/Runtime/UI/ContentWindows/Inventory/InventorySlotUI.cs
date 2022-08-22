using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using Entity.InventorySystem;
    
    public class InventorySlotUI : MonoBehaviour, IWindowElement
    {
        [Header("References")] [SerializeField]
        private Image _itemIcon;

        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemAmount;

        [Header("Button")] [SerializeField] private Button _button;
        [SerializeField] private Color _highlightColor;

        private IWindow _window;

        public InventoryWindow.ItemCategory ItemCategory { get; private set; }
        public ItemSlot ItemSlot { get; private set; }

        #region UnityEvents

        private void Start()
        {
            var colorBlock = _button.colors;
            colorBlock.highlightedColor = _highlightColor;
            colorBlock.selectedColor = _highlightColor;
            _button.colors = colorBlock;
        }

        #endregion

        public void SetSlotValues(IWindow window, ItemSlot item)
        {
            // Assign
            _window = window;
            ItemSlot = item;
            ItemCategory = InventoryWindow.GetCategory(item.Item.GetType());

            _itemIcon.sprite = item.Item.Icon;
            _itemName.text = item.Item.Name;
            _itemAmount.text = item.Amount.ToString();

            _itemAmount.enabled = item.Amount > 1;
            _itemIcon.enabled = item.Item.Icon;
        }

        public void OnSelect()
        {
            _window.Select(this);
        }
    }
}
