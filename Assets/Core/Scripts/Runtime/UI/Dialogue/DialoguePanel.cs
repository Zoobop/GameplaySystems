using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    using DialogueSystem;
    using Utility.ExtensionMethods;
    
    public class DialoguePanel : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Canvas _canvas;

        [SerializeField] private Transform _contentHolder;
        [SerializeField] private TextMeshProUGUI _speakerDialogue;
        [SerializeField] private TextMeshProUGUI _nextButtonText;
        [SerializeField] private GameObject _dialogueOptionPrefab;

        private readonly List<DialogueOptionUI> _dialogueOptions = new();
        private static readonly string SpeakerDialogueFormat = "{0} - {1}";
        private static readonly string OptionFormat = "{0}. {1}";
        private const string Next = "[Next]";
        private const string Leave = "[Leave]";

        private string _speaker;
        private Dialogue _dialogue;
        

        #region PanelUtility

        public void SetDialogue(string speaker, Dialogue dialogue)
        {
            // Assign
            _speaker = speaker;
            _dialogue = dialogue;

            NextDialogue();
        }

        private void NextDialogue()
        {
            // Check if basic dialogue
            if (_dialogue.DialogueType == DialogueType.SingleChoice)
            {
                // Enable next button
                var text = _dialogue.IsEndingDialogue ? Leave : Next;
                ActivateNextButton(text);

                // Disable options visibility
                SetOptionsVisibility(false);

                // Update text
                _speakerDialogue.text = string.Format(SpeakerDialogueFormat, _speaker, _dialogue.Text);
                return;
            }

            // If dialogue has options

            // Disable next button
            DeactivateNextButton();

            // Enable options visibility
            SetOptionsVisibility(true);

            // Update text
            _speakerDialogue.text = string.Format(SpeakerDialogueFormat, _speaker, _dialogue.Text);

            // Update dialogue options
            SetupOptions(_dialogue.Choices);
        }

        private void SetupOptions(IEnumerable<DialogueChoiceData> dialogueOptions)
        {
            // Remove current options
            foreach (var option in _dialogueOptions)
            {
                Destroy(option.gameObject);
            }

            _dialogueOptions.Clear();

            // Add new options
            var optionNumber = 1;
            foreach (var dialogueOption in dialogueOptions)
            {
                // Check dialogue condition
                if (MeetsCondition(dialogueOption.NextDialogue))
                {
                    var optionUI = Instantiate(_dialogueOptionPrefab, _contentHolder).GetComponent<DialogueOptionUI>();
                    optionUI.Assign(string.Format(OptionFormat, optionNumber++, dialogueOption.Text),
                        dialogueOption.NextDialogue);
                    _dialogueOptions.Add(optionUI);
                }
            }
        }

        private bool MeetsCondition(Dialogue dialogue)
        {
            return DialogueController.Instance.CheckDialogueCondition(dialogue);
        }

        private void SetOptionsVisibility(bool state)
        {
            if (_dialogueOptions.IsEmpty()) return;

            foreach (var option in _dialogueOptions)
            {
                option.gameObject.SetActive(state);
            }
        }

        public void OnClickedNext()
        {
            DialogueController.Instance.NextDialogue(_dialogue.Choices[0].NextDialogue);
        }

        private void ActivateNextButton(string text)
        {
            _nextButtonText.gameObject.SetActive(true);
            _nextButtonText.text = text;
        }

        private void DeactivateNextButton()
        {
            _nextButtonText.gameObject.SetActive(false);
        }
        
        public void Enable()
        {
            _canvas.enabled = true;
        }

        public void Disable()
        {
            _canvas.enabled = false;
        }

        #endregion
    }
}
