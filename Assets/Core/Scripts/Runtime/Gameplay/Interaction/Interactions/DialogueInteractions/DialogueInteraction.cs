using UnityEngine;

namespace InteractionSystem
{
    using Entity;
    using DialogueSystem;
    using InputSystem;
    using UI;
    using LocalizationSystem;
    
    public class DialogueInteraction : Interaction
    {
        [Header("Dialogue")] [SerializeField] private DialogueArchive _dialogueArchive;

        private ICharacter _character;

        #region UnityEvents

        private void Awake()
        {
            TryGetCharacter();
        }

        private void OnValidate()
        {
            TryGetCharacter();

            if (_character == null)
            {
                Debug.LogWarning($"({transform.parent.parent.name}) Cannot find an attached Character component to get dialogue!");
            }
        }

        private void Start()
        {
            _dialogueArchive.AddEndEvent(StopInteraction);
        }

        private void OnDisable()
        {
            _dialogueArchive.RemoveEndEvent(StopInteraction);
        }

        #endregion

        #region Interaction

        public override bool MeetsCondition(in ICharacter character)
        {
            return _dialogueArchive is not null && _dialogueArchive.HasDialogue();
        }

        public override string GetPrimaryInteractionText()
        {
            return LocalizationSystem.GetLocalizedValue("ui_interaction_dialogue");
        }

        public override void OnStart()
        {
            base.OnStart();

            InputController.DisableMovementActions();
        }

        public override void OnInteract(in ICharacter character)
        {
            GameEventLog.LogEvent($"Starting dialogue with {_character.GetName()}");
            DialogueController.Instance.StartDialogue(_character);
        }

        public override void OnEnd()
        {
            InputController.EnableMovementActions();

            base.OnEnd();
        }

        #endregion

        #region Helpers

        private void TryGetCharacter()
        {
            // Try get character
            _character = GetComponentInParent<ICharacter>();

            // Get dialogue archive if found
            if (_character != null)
            {
                _dialogueArchive = _character.GetDialogueArchive() as DialogueArchive;
            }
        }

        #endregion
    }
}