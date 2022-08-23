using System;
using UnityEngine;

namespace InteractionSystem
{
    using Entity;
    using Entity.Item;

    [Serializable]
    public class RequiredItem : UnlockCondition
    {
        [SerializeField] private Item _requiredItem;

        protected override void OnValidate()
        {
            // Return if not null
            if (_requiredItem) return;

            // Parent implementation
            base.OnValidate();
        }

        public override bool IsConditionMet(ICharacter character)
        {
            // Get entity's inventory
            var inventory = character.GetInventory();
            return inventory is not null && inventory.HasItem(_requiredItem);
        }
    }
}