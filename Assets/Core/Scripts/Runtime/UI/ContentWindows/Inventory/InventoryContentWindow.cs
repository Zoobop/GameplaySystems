using System.Collections;
using UnityEngine;

namespace UI
{
    using Entity.InventorySystem;
    using InputSystem;
    
    public class InventoryContentWindow : ContentWindow<IInventory>
    {
        public static InventoryContentWindow Instance { get; private set; }

        [Header("References")] [SerializeField]
        private InventoryPanel _playerInventoryPanel;

        [SerializeField] private InventoryPanel _otherInventoryPanel;

        private IInventory _playerInventory;
        private IInventory _otherInventory;
        private InventorySlotUI _currentSelected;

        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            Instance = this;

            _playerInventoryPanel.SetMoveEvent(MoveItemToOtherInventory);
            _playerInventoryPanel.SetMoveAllEvent(MoveItemsToOtherInventory);
            _otherInventoryPanel.SetMoveEvent(MoveItemToPlayerInventory);
            _otherInventoryPanel.SetMoveAllEvent(MoveItemsToPlayerInventory);
        }

        #endregion

        #region WindowUtility

        public void Bind(IInventory inventory)
        {
            // Unbind previous
            if (_playerInventory is not null)
            {
                _playerInventory.OnInventoryChanged -= _playerInventoryPanel.OnInventoryChangedCallback;
            }

            // Assign new
            _playerInventory = inventory;

            // Bind
            _playerInventory.OnInventoryChanged += _playerInventoryPanel.OnInventoryChangedCallback;
            _playerInventoryPanel.OnInventoryChangedCallback(_playerInventory);
        }

        private void OpenInventory(IInventory inventory)
        {
            // Assign new
            _otherInventory = inventory;

            // Bind
            _otherInventory.OnInventoryChanged += _otherInventoryPanel.OnInventoryChangedCallback;
            _otherInventoryPanel.OnInventoryChangedCallback(_otherInventory);
        }

        private void CloseInventory()
        {
            // Unbind previous
            _otherInventory.OnInventoryChanged -= _otherInventoryPanel.OnInventoryChangedCallback;

            // Nullify
            _otherInventory = null;
        }

        #endregion

        #region IContentWindow

        protected override IEnumerator Show(IInventory content)
        {
            // Set window visibility
            SetCanvas(true);
            SetWindowOpen(true);

            // Open inventory with content
            OpenInventory(content);
            // Deselect any UI elements
            Deselect();

            // Input handling
            InputController.DisableInteractionActions();
            InputController.EnableGeneralUIActions();

            yield return base.Show(content);
        }

        protected override IEnumerator Hide()
        {
            yield return base.Hide();

            // Close inventory
            CloseInventory();
            // Deselect any UI elements
            Deselect();

            // Input handling
            InputController.EnableInteractionActions();
            InputController.DisableGeneralUIActions();

            // Set window visibility
            SetCanvas(false);
            SetWindowOpen(false);
        }

        #endregion

        #region ButtonCallbacks

        private void MoveItemToPlayerInventory()
        {
            var (item, amount) = _otherInventory.Transfer(_currentSelected.ItemSlot.Item);
            _playerInventory.AddItem(item, amount);
            Deselect();
        }

        private void MoveItemToOtherInventory()
        {
            var (item, amount) = _playerInventory.Transfer(_currentSelected.ItemSlot.Item);
            _otherInventory.AddItem(item, amount);
            Deselect();
        }

        private void MoveItemsToPlayerInventory()
        {
            _playerInventory.AddItems(_otherInventory.TransferAll());
            Deselect();
        }

        private void MoveItemsToOtherInventory()
        {
            _otherInventory.AddItems(_playerInventory.TransferAll());
            Deselect();
        }

        #endregion

        #region IWindow

        public override void Select(IWindowElement element)
        {
            var slot = element as InventorySlotUI;

            if (slot.transform.IsChildOf(_playerInventoryPanel.GetContentHolder()))
            {
                _playerInventoryPanel.EnableMove();
                _otherInventoryPanel.DisableMove();
            }
            else if (slot.transform.IsChildOf(_otherInventoryPanel.GetContentHolder()))
            {
                _playerInventoryPanel.DisableMove();
                _otherInventoryPanel.EnableMove();
            }
            else
            {
                Deselect();
            }

            _currentSelected = slot;
        }

        public override void Deselect()
        {
            _playerInventoryPanel.DisableMove();
            _otherInventoryPanel.DisableMove();

            _currentSelected = null;
        }

        #endregion
    }
}
