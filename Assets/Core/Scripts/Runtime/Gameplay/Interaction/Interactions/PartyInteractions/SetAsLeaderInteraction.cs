


namespace InteractionSystem
{

    using Entity;
    using Entity.Groups;
    using Core.LocalizationSystem;
    
    public class SetAsLeaderInteraction : Interaction
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
            return PlayerPartyController.Instance.IsActivePartyMember(_character);
        }

        public override string GetPrimaryInteractionText()
        {
            return LocalizationSystem.GetLocalizedValue("ui_interaction_partyleader");
        }

        public override void OnInteract(in ICharacter interactionInitiator)
        {
            print($"{_character.GetName()} is now the party leader!");

            // Set as party leader
            GameManager.SetCurrentPlayer(_character);

            StopInteraction();
        }

        #endregion
    }
}