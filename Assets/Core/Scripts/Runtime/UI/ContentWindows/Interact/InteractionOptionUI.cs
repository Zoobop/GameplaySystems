

namespace UI
{
    using InteractionSystem;
    
    public class InteractionOptionUI : UIOptionElement<IInteraction>
    {
        private IInteraction _interaction;

        public override void Assign(string formattedText, in IInteraction context)
        {
            _interaction = context;
            _optionText.text = formattedText;
        }

        protected override void OnSelected()
        {
            InteractionController.Instance.StartInteraction(_interaction);
            InteractContentWindow.Instance.CloseWindow();
        }
    }
}
