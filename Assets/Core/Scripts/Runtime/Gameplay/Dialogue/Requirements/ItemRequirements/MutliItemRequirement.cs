using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    using Entity;
    using Entity.Item;
    
    public class MultiItemRequirement : DialogueRequirement
    {
        [Serializable]
        private struct RequiredItem
        {
            public Item requiredItem;
            public int requiredAmount;

            public void Deconstruct(out Item requiredItem, out int requiredAmount)
            {
                requiredItem = this.requiredItem;
                requiredAmount = this.requiredAmount;
            }
        }

        [Header("Required Items")] 
        [SerializeField] private List<RequiredItem> _requiredItems = new();
        [SerializeField] private bool _willRemoveItem = true;

        public override bool IsConditionMet(ICharacter character)
        {
            foreach (var (requiredItem, requiredAmount) in _requiredItems)
            {
                if (!character.GetInventory().HasItem(requiredItem, requiredAmount))
                {
                    return false;
                }
            }

            return true;
        }

        public override void InvokeAction(ICharacter character)
        {
            // Parent implementation
            base.InvokeAction(character);

            // Return if don't remove
            if (!_willRemoveItem) return;

            // Remove items
            foreach (var (requiredItem, requiredAmount) in _requiredItems)
            {
                character.GetInventory().RemoveItem(requiredItem, requiredAmount);
            }
        }
    }
}