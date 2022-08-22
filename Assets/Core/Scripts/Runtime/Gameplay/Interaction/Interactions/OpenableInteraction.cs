

namespace InteractionSystem
{
    using Entity;

    public class OpenableInteraction : Interaction
    {
        private IOpenable _openable;

        #region UnityEvents

        private void Awake()
        {
            _openable = GetComponentInParent<IOpenable>();
        }

        private void OnValidate()
        {
            _openable = GetComponentInParent<IOpenable>();
        }

        #endregion

        public override bool MeetsCondition(in ICharacter character)
        {
            return true;
        }

        public override string GetPrimaryInteractionText()
        {
            return "Open";
        }

        public override void OnInteract(in ICharacter character)
        {
            _openable.Toggle();

            StopInteraction();
        }
    }
}