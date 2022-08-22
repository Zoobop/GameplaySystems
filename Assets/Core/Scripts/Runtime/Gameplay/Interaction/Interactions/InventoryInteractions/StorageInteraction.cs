

namespace InteractionSystem
{
    using Entity;

    public class StorageInteraction : InventoryInteraction
    {
        private IOpenable _openable;

        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            _openable = GetComponentInParent<IOpenable>();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            _openable = GetComponentInParent<IOpenable>();
        }

        #endregion

        public override bool MeetsCondition(in ICharacter character)
        {
            return _openable != null && base.MeetsCondition(character);
        }

        public override void OnStart()
        {
            base.OnStart();

            _openable.Open();
        }

        public override void OnInteract(in ICharacter character)
        {
            _inventory.Name = _openable.GetName();

            base.OnInteract(in character);
        }

        public override void OnEnd()
        {
            base.OnEnd();

            _openable.Close();
        }
    }
}