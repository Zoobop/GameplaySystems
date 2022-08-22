

using System.Collections.Generic;

namespace DialogueSystem
{
    public interface IDialogueArchive
    {
        public Dialogue GetCurrentDialogue();
        public IDictionary<Dialogue, DialogueRequirement> GetRequirements();
        public void OnDialogueStart();
        public void OnDialogueEnd();
    }
}