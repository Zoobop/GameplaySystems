

namespace InteractionSystem
{

    using Entity;
    
    public class LootOnlyInteraction : InventoryInteraction
    {
        private IOpenable _openable;

        #region UnityEvents

        protected override void Awake()
        {
            // Parent implementation
            base.Awake();

            _openable = GetComponentInParent<IOpenable>();
        }

        protected override void OnValidate()
        {
            // Parent implementation
            base.OnValidate();

            _openable = GetComponentInParent<IOpenable>();
        }

        #endregion

        public override string GetPrimaryInteractionText()
        {
            return "Loot";
        }

        public override void OnStart()
        {
            InvokeStart();
        }

        public override void OnInteract(in ICharacter character)
        {
            var items = _inventory.TransferAll();
            character.GetInventory().AddItems(items);

            _openable.Open();
            SetActive(false);
            StopInteraction();
        }

        public override void OnEnd()
        {
            InvokeStart();
        }
    }
}