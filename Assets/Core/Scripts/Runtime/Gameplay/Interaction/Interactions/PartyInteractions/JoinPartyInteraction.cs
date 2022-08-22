

namespace InteractionSystem
{
    using Entity;
    using Entity.Groups;
    using Core.LocalizationSystem;
    
    public class JoinPartyInteraction : Interaction
    {
        private ICharacter _character;

        #region UnityEvents

        private void Awake()
        {
            _character = GetComponentInParent<ICharacter>();
        }

        private void OnValidate()
        {
            _character = GetComponentInParent<ICharacter>();
        }

        #endregion

        #region Interaction

        public override bool MeetsCondition(in ICharacter interactionInitiator)
        {
            return !PlayerPartyController.Instance.IsPartyMember(_character);
        }

        public override string GetPrimaryInteractionText()
        {
            return LocalizationSystem.GetLocalizedValue("ui_interaction_party_add");
        }

        public override void OnInteract(in ICharacter interactionInitiator)
        {
            print($"{_character.GetName()} has joined the party!");

            // Add character to party
            PlayerPartyController.Instance.AddToParty(_character);

            StopInteraction();
        }

        #endregion
    }
}