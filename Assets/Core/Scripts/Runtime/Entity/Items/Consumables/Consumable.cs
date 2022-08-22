using UnityEngine;

using UI;

namespace Entity
{
    [CreateAssetMenu(menuName = "Items/Consumable", fileName = "Consumable_", order = 0)]
    public class Consumable : Item, IUsable
    {
        public bool Use(int amount = 1)
        {
            var message = $"Used {amount} of {_name}!";
            Debug.Log(message);
            GameEventLog.LogEvent(message);
            return true;
        }
    }
}