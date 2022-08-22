using System.Collections;
using UnityEngine;

namespace UI
{
    using Entity;
    using Entity.InventorySystem;
    
    public class PlayerMenuController : Controller
    {
        public static PlayerMenuController Instance { get; private set; }

        [Header("Player Menus")] [SerializeField]
        private PlayerProfileUI _playerProfile;

        [SerializeField] private InventoryWindow _inventoryWindow;
        [SerializeField] private SkillTreeUI _skillTreeUI;
        [SerializeField] private SettingsUI _settingsUI;

        private IInventory _inventory;

        public static PlayerProfileUI PlayerProfile { get; private set; }
        public static InventoryWindow InventoryWindow { get; private set; }
        public static SkillTreeUI SkillTreeUI { get; private set; }
        public static SettingsUI SettingsUI { get; private set; }

        private Coroutine _coroutine;

        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            Instance = this;

            PlayerProfile = _playerProfile;
            InventoryWindow = _inventoryWindow;
            SkillTreeUI = _skillTreeUI;
            SettingsUI = _settingsUI;
        }

        #endregion

        public void Bind(ICharacter player)
        {
            // Unbind previous
            Unbind();

            // Assign
            _inventory = player.GetInventory();

            // Rebind
            Rebind();
        }

        private void Unbind()
        {
            // Unbind previous
            if (_inventory is not null)
            {
                _inventory.OnInventoryChanged -= _inventoryWindow.Bind;
            }
        }

        private void Rebind()
        {
            // Rebind
            _inventory.OnInventoryChanged += _inventoryWindow.Bind;
            _inventoryWindow.Bind(_inventory);
        }

        public void ToggleInventory()
        {
            // Get window state
            var state = _inventoryWindow.IsWindowActive;
            if (!state)
            {
                // Is not open
                Open();
                return;
            }

            // Is open
            Close();
        }

        public void Open()
        {
            if (_coroutine != null)
            {
                StopCoroutine(WaitForShowCoroutine());
            }

            StartCoroutine(WaitForShowCoroutine());
        }

        public void Close()
        {
            if (_coroutine != null)
            {
                StopCoroutine(WaitForHideCoroutine());
            }

            StartCoroutine(WaitForHideCoroutine());
        }

        private IEnumerator WaitForShowCoroutine()
        {
            _inventoryWindow.OpenWindow();

            yield return Show();
        }

        private IEnumerator WaitForHideCoroutine()
        {
            yield return Hide();

            _inventoryWindow.CloseWindow();
        }
    }
}
