using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    using Entity;
    using InteractionSystem;

    public abstract class DialogueRequirement : MonoBehaviour, IUnlockCondition
    {
        [field: Header("Dialogue")] 
        [field: SerializeField] public Dialogue DialogueToCheck { get; set; }

        [Header("Requirement Actions")] [SerializeField]
        protected UnityEvent _events;

        public abstract bool IsConditionMet(ICharacter character);

        public virtual void InvokeAction(ICharacter character)
        {
            // Invoke events
            _events?.Invoke();
        }
    }
}