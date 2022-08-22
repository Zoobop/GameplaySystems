
namespace Entity
{
    using DialogueSystem;
    using InteractionSystem;
    using InventorySystem;
    
    public interface ICharacter : IEntity
    {
        public string GetFullName();
        public IStats GetStats();
        public ILevelProgression GetLevelProgression();
        public IInventory GetInventory();
        public SkillBag GetSkillBag();
        public ICharacterMovement GetMovement();
        public IInteractionInitiator GetInteractionInitiator();
        public IDialogueArchive GetDialogueArchive();
    }
}