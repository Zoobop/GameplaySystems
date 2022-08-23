using System;
using UnityEngine;

namespace Entity
{
    public enum StatAttributeType
    {
        Xp,
        Health,
        Stamina
    }

    [Serializable]
    public class StatAttributeElement
    {
        [SerializeField] private StatAttributeType _statType;
        [SerializeField] private string _statName;
        [SerializeField] private int _minimumAmount;
        [SerializeField] private int _currentAmount;
        [SerializeField] private int _maximumAmount;

        public StatAttributeType StatType => _statType;
        public string Name => _statName;
        public int Minimum => _minimumAmount;
        public int Current => _currentAmount;
        public int Maximum => _maximumAmount;
        public float Ratio => (float) (_currentAmount + _minimumAmount) / (float) (_maximumAmount - _minimumAmount);

        public event Action<StatAttributeElement> OnStatChanged = delegate { };

        public StatAttributeElement()
        {
            _statType = StatAttributeType.Health;
            _statName = "Stat";
            _minimumAmount = 0;
            _currentAmount = 100;
            _maximumAmount = 100;
        }

        public StatAttributeElement(StatAttributeElement other)
        {
            _statType = other._statType;
            _statName = other._statName;
            _minimumAmount = other._minimumAmount;
            _maximumAmount = other._maximumAmount;
            _currentAmount = other._currentAmount;
        }

        public StatAttributeElement(StatAttributeType type, string name, int min, int max)
        {
            _statType = type;
            _statName = name;
            _minimumAmount = min;
            _maximumAmount = max;
            _currentAmount = min;
        }

        public StatAttributeElement Modify(int modification)
        {
            // Add the modification amount
            _currentAmount += modification;

            // Clamp between minimum and maximum amount
            _currentAmount = Mathf.Clamp(_currentAmount, _minimumAmount, _maximumAmount);

            // Event invoke
            OnStatChanged?.Invoke(this);
            return this;
        }

        public void SetFields(int min, int current, int max)
        {
            _minimumAmount = min;
            _currentAmount = current;
            _maximumAmount = max;
        }
    }
}