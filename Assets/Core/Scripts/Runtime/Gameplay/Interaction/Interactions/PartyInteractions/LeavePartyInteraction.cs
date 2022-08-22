

namespace InteractionSystem
{
    
    using Entity;
    using Entity.Groups;
    using Core.LocalizationSystem;
    
    public class LeavePartyInteraction : Interaction
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
            return PlayerPartyController.Instance.IsPartyMember(_character);
        }

        public override string GetPrimaryInteractionText()
        {
            return LocalizationSystem.GetLocalizedValue("ui_interaction_party_remove");
        }

        public override void OnInteract(in ICharacter interactionInitiator)
        {
            print($"{_character.GetName()} has left the party!");

            // Remove character from party
            PlayerPartyController.Instance.RemoveFromParty(_character);

            StopInteraction();
        }

        #endregion
    }
}