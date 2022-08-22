namespace InteractionSystem
{

    using Entity;

    public interface IUnlockCondition
    {
        public bool IsConditionMet(ICharacter character);
    }
}