
namespace InteractionSystem
{
    public interface IInteractionInitiator
    {
        public void StartInteracting();
        public void StopInteracting();
        public bool IsInteracting();
    }
}