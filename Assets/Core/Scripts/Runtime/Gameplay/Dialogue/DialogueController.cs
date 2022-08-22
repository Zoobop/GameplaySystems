using UnityEngine;

using Entity;
using UI;

namespace DialogueSystem
{
    
    public class DialogueController : Controller
    {
        public static DialogueController Instance { get; private set; }

        [Header("References")] [SerializeField]
        private DialoguePanel _dialoguePanel;

        private ICharacter _player;
        private ICharacter _speaker;

        #region UnityEvents

        protected override void Awake()
        {
            base.Awake();

            Instance = this;

            GameManager.OnPlayerChanged += OnPlayerChangedCallback;
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _player = player;
        }

        private void EndDialogue()
        {
            // Invoke event
            _speaker.GetDialogueArchive().OnDialogueEnd();

            // Hide panel
            _speaker = null;
            _dialoguePanel.Disable();

            UIController.Instance.ShowHUD();
            Hide();
        }

        public void StartDialogue(ICharacter character)
        {
            // Assign
            _speaker = character;

            var dialogue = _speaker.GetDialogueArchive().GetCurrentDialogue();

            // Show dialogue panel
            _dialoguePanel.Enable();
            NextDialogue(dialogue);

            UIController.Instance.HideHUD();
            Show();

            // Invoke event
            _speaker.GetDialogueArchive().OnDialogueStart();
        }

        public void NextDialogue(Dialogue dialogue)
        {
            // Null check dialogue
            if (dialogue is null)
            {
                EndDialogue();
                return;
            }

            // Invoke action if valid
            TryInvokeDialogueAction(dialogue);

            // Set next dialogue
            _dialoguePanel.SetDialogue(_speaker.GetName(), dialogue);
        }

        public bool CheckDialogueCondition(Dialogue dialogue)
        {
            // Null check
            if (dialogue is null) return true;

            var map = _speaker.GetDialogueArchive().GetRequirements();
            if (map.TryGetValue(dialogue, out var requirement))
            {
                // Check condition
                return requirement.IsConditionMet(_player);
            }

            // If get here, dialogue does not have condition
            return true;
        }

        private void TryInvokeDialogueAction(Dialogue dialogue)
        {
            if (_speaker.GetDialogueArchive().GetRequirements().TryGetValue(dialogue, out var requirement))
            {
                requirement.InvokeAction(_player);
            }
        }
    }
}
