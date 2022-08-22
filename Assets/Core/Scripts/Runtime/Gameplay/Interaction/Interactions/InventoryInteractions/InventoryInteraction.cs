

namespace InteractionSystem
{
    using Entity;
    using Entity.InventorySystem;
    using InputSystem;
    using UI;
    using Core.LocalizationSystem;

    public class InventoryInteraction : Interaction
    {
        protected IInventory _inventory;

        #region UnityEvents

        protected virtual void Awake()
        {
            _inventory = transform.parent.GetComponentInChildren<IInventory>();
        }

        protected virtual void OnValidate()
        {
            _inventory = transform.parent.GetComponentInChildren<IInventory>();
        }

        protected virtual void Start()
        {
            InventoryContentWindow.Instance.OnWindowClosed += StopInteraction;
        }

        protected virtual void OnDestroy()
        {
            InventoryContentWindow.Instance.OnWindowClosed -= StopInteraction;
        }

        #endregion

        #region Interaction

        public override bool MeetsCondition(in ICharacter character)
        {
            return _inventory != null;
        }

        public override string GetPrimaryInteractionText()
        {
            return LocalizationSystem.GetLocalizedValue("ui_interaction_inventory");
        }

        public override void OnStart()
        {
            base.OnStart();

            InputController.Instance.DisableMovementActions();
        }

        public override void OnInteract(in ICharacter character)
        {
            GameEventLog.LogEvent($"Now opening {_inventory.Name}");

            // Open an inventory window using the passed inventory
            InventoryContentWindow.Instance.OpenWindow(_inventory);
        }

        public override void OnEnd()
        {
            base.OnEnd();

            InputController.Instance.EnableMovementActions();
        }

        #endregion
    }
}