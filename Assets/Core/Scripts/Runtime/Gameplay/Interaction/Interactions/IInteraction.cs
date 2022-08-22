using System;

namespace InteractionSystem
{
    using Entity;
    
    public interface IInteraction
    {
        public event Action OnStarted;
        public event Action OnEnded;

        public bool MeetsCondition(in ICharacter interactionInitiator);
        public bool IsActive();
        public string GetPrimaryInteractionText();
        public string GetSecondaryInteractionText();
        public void OnStart();
        public void OnInteract(in ICharacter interactionInitiator);
        public void OnEnd();
        public void PostInteraction();
    }
}
