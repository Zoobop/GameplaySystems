using UnityEngine;

namespace Entity
{
    public abstract class Item : ScriptableObject
    {
        [Header("General")] [SerializeField] protected string _name = "New Item";
        [TextArea] [SerializeField] protected string _description = "This is where the item's description is written.";
        [TextArea] [SerializeField] protected string _lore = "This is where the item's lore is written.";
        [Min(1)] [SerializeField] protected int _maxStack = 1;
        [SerializeField] protected bool _isDiscardable;
        [SerializeField] protected bool _isSellable;
        [Min(0)] [SerializeField] protected float _weight;
        [Min(0)] [SerializeField] protected int _value;

        [Header("Appearance")] [SerializeField]
        protected Sprite _icon;

        [SerializeField] protected GameObject _prefab;

        public string Name => _name;
        public string Description => _description;
        public string Lore => _lore;
        public int MaxStack => _maxStack;
        public bool IsStackable => _maxStack > 1;
        public bool IsDiscardable => _isDiscardable;
        public bool IsSellable => _isSellable;
        public float Weight => _weight;
        public int Value => _value;

        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
    }
}
