using UnityEngine;

using Entity;

namespace DialogueSystem
{
    public class SingleItemRequirement : DialogueRequirement
    {
        [Header("Required Item")] 
        [SerializeField] private Item _requiredItem;
        [SerializeField, Min(1)] private int _requiredAmount = 1;
        [SerializeField] private bool _willRemoveItem = true;

        public override bool IsConditionMet(ICharacter character)
        {
            return character.GetInventory().HasItem(_requiredItem, _requiredAmount);
        }

        public override void InvokeAction(ICharacter character)
        {
            // Parent implementation
            base.InvokeAction(character);

            // Remove item?
            if (_willRemoveItem)
            {
                character.GetInventory().RemoveItem(_requiredItem, _requiredAmount);
            }
        }
    }
}