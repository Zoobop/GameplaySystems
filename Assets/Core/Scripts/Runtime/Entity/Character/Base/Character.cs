using UnityEngine;

namespace Entity
{
    using InventorySystem;
    using DialogueSystem;
    using InteractionSystem;
    using Core.LocalizationSystem;

    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected GameObject _model;
        
        [SerializeField] protected Stats _stats;
        [SerializeField] protected LevelProgression _levelProgression;
        [SerializeField] protected Inventory _inventory;
        [SerializeField] protected SkillBag _skillBag;
        [SerializeField] protected CharacterMovement _movement;
        [SerializeField] protected InteractionHandler _interactionHandler;
        [SerializeField] protected DialogueArchive _dialogueArchive;
        
        [SerializeField] protected LocalizedString _title = string.Empty;
        [SerializeField] protected LocalizedString _name = string.Empty;
        [SerializeField] protected string _fullName = string.Empty;
        [SerializeField] private int _fullNameFormatIndex = 1;
        [Min(0)] 
        [SerializeField] protected int _gold;
        
        public int Gold => _gold;

        #region ICharacter

        public string GetName()
        {
            return _name;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Transform Spawn(Transform parent = null)
        {
            if (parent != null)
            {
                return Instantiate(transform);
            }

            return Instantiate(transform, parent);
        }

        public void Despawn(bool destroy = true)
        {
            if (destroy)
            {
                Destroy(gameObject);
                return;
            }

            gameObject.SetActive(false);
        }

        public string GetFullName()
        {
            return _fullName;
        }

        public IStats GetStats()
        {
            return _stats;
        }

        public ILevelProgression GetLevelProgression()
        {
            return _levelProgression;
        }

        public IInventory GetInventory()
        {
            return _inventory;
        }

        public SkillBag GetSkillBag()
        {
            return _skillBag;
        }

        public ICharacterMovement GetMovement()
        {
            return _movement;
        }

        public IInteractionInitiator GetInteractionInitiator()
        {
            return _interactionHandler;
        }

        public IDialogueArchive GetDialogueArchive()
        {
            return _dialogueArchive;
        }

        #endregion
    }
}