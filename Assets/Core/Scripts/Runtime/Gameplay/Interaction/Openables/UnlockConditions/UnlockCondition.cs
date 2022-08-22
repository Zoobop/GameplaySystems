using UnityEngine;

namespace InteractionSystem
{
    using Entity;

    public abstract class UnlockCondition : MonoBehaviour, IUnlockCondition
    {
        protected virtual void OnValidate()
        {
            Debug.LogWarning($"Unlock condition \"{GetType().Name}\" has not been set!");
        }

        public abstract bool IsConditionMet(ICharacter character);
    }
}