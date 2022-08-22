using UnityEngine;

namespace UI
{
    using Entity;
    using Entity.InventorySystem;
    using InteractionSystem;
    
    public class ContentWindowsController : Controller
    {
        public static ContentWindowsController Instance { get; private set; }

        [Header("Content Windows")] [SerializeField]
        private InventoryContentWindow _inventoryWindow;

        [SerializeField] private InteractContentWindow _interactWindow;

        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            Instance = this;
        }

        #endregion

        public void Bind(ICharacter player)
        {
            // Bind if not null
            if (_inventoryWindow)
            {
                _inventoryWindow.Bind(player.GetInventory());
            }
        }

        #region InventoryContentWindow

        public void OpenInventoryWindow(IInventory inventory)
        {
            _inventoryWindow.OpenWindow(inventory);
        }

        public void CloseInventoryWindow()
        {
            _inventoryWindow.CloseWindow();
        }

        #endregion

        #region InteractContentWindow

        public void OpenInteractWindow(IInteractionTarget target)
        {
            _interactWindow.OpenWindow(target);
        }

        public void CloseInteractWindow()
        {
            _interactWindow.CloseWindow();
        }

        #endregion
    }
}