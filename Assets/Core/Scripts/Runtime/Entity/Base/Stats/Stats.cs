
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    public class Stats : MonoBehaviour, IStats
    {
        [Header("Stat Attributes")] [SerializeField]
        protected StatAttributeElement _healthStatElement = new(StatAttributeType.Health, "Vitality", 0, 100);

        [SerializeField]
        protected StatAttributeElement _staminaStatElement = new(StatAttributeType.Stamina, "Endurance", 0, 100);

        public event Action<IStats> OnStatsChanged = delegate { };

        public virtual void ModifyStat(StatAttributeType type, int modification)
        {
            if (type == StatAttributeType.Health) _healthStatElement.Modify(modification);
            if (type == StatAttributeType.Stamina) _staminaStatElement.Modify(modification);

            // Invoke event
            OnStatsChanged?.Invoke(this);
        }

        public StatAttributeElement GetStat(StatAttributeType type)
        {
            if (type == StatAttributeType.Health) return _healthStatElement;
            if (type == StatAttributeType.Stamina) return _staminaStatElement;
            throw new ArgumentOutOfRangeException();
        }

        public virtual IList<StatAttributeElement> AsList()
        {
            return new List<StatAttributeElement> {_healthStatElement, _staminaStatElement};
        }

        public virtual IDictionary<StatAttributeType, StatAttributeElement> AsDictionary()
        {
            return new Dictionary<StatAttributeType, StatAttributeElement>
            {
                {_healthStatElement.StatType, _healthStatElement},
                {_staminaStatElement.StatType, _staminaStatElement},
            };
        }

        protected void Update()
        {
            if (Keyboard.current.digit1Key.wasReleasedThisFrame)
            {
                Add25Health();
            }

            if (Keyboard.current.digit2Key.wasReleasedThisFrame)
            {
                Add50Health();
            }

            if (Keyboard.current.digit3Key.wasReleasedThisFrame)
            {
                Remove25Health();
            }

            if (Keyboard.current.digit4Key.wasReleasedThisFrame)
            {
                Remove50Health();
            }
        }

        [ContextMenu("Add 25")]
        private void Add25Health()
        {
            ModifyStat(StatAttributeType.Health, 25);
        }

        [ContextMenu("Add 50")]
        private void Add50Health()
        {
            ModifyStat(StatAttributeType.Health, 50);
        }

        [ContextMenu("Remove 25")]
        private void Remove25Health()
        {
            ModifyStat(StatAttributeType.Health, -25);
        }

        [ContextMenu("Remove 50")]
        private void Remove50Health()
        {
            ModifyStat(StatAttributeType.Health, -50);
        }
    }
}