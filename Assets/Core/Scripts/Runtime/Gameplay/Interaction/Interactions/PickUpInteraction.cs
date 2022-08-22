

namespace InteractionSystem
{

    using Entity;
    
    public class PickUpInteraction : Interaction
    {
        private ICollectable _collectable;

        #region UnityEvents

        private void Awake()
        {
            _collectable = GetComponentInParent<ICollectable>();
        }

        private void OnValidate()
        {
            _collectable = GetComponentInParent<ICollectable>();
        }

        #endregion

        #region Interaction

        public override bool MeetsCondition(in ICharacter character)
        {
            return true;
        }

        public override string GetPrimaryInteractionText()
        {
            return "Collect";
        }

        public override void OnInteract(in ICharacter character)
        {
            var (item, amount) = _collectable.Collect();
            character.GetInventory().AddItem(item, amount);

            SetActive(false);

            _collectable.Despawn(false);
            StopInteraction();
        }

        #endregion
    }
}