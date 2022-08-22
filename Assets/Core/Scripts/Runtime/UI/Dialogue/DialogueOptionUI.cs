
using DialogueSystem;

namespace UI
{
    public class DialogueOptionUI : UIOptionElement<Dialogue>
    {
        private Dialogue _dialogue;

        public override void Assign(string formattedText, in Dialogue content)
        {
            // Assign
            _dialogue = content;
            _optionText.text = formattedText;
        }

        protected override void OnSelected()
        {
            DialogueController.Instance.NextDialogue(_dialogue);
        }
    }
}
