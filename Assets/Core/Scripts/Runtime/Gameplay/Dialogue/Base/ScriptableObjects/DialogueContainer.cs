using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueContainer : ScriptableObject
    {
        [field: SerializeField] public string FileName { get; set; }
        [field: SerializeField] public SerializableDictionary<DialogueGroup, List<Dialogue>> DialogueGroups { get; set; }
        [field: SerializeField] public List<Dialogue> UngroupedDialogues { get; set; }

        public void Initialize(string fileName)
        {
            FileName = fileName;

            DialogueGroups = new SerializableDictionary<DialogueGroup, List<Dialogue>>();
            UngroupedDialogues = new List<Dialogue>();
        }

        public List<string> GetDialogueGroupNames()
        {
            List<string> dialogueGroupNames = new List<string>();

            foreach (DialogueGroup dialogueGroup in DialogueGroups.Keys)
            {
                dialogueGroupNames.Add(dialogueGroup.GroupName);
            }

            return dialogueGroupNames;
        }

        public List<string> GetGroupedDialogueNames(DialogueGroup dialogueGroup, bool startingDialoguesOnly)
        {
            List<Dialogue> groupedDialogues = DialogueGroups[dialogueGroup];

            List<string> groupedDialogueNames = new List<string>();

            foreach (Dialogue groupedDialogue in groupedDialogues)
            {
                if (startingDialoguesOnly && !groupedDialogue.IsStartingDialogue)
                {
                    continue;
                }

                groupedDialogueNames.Add(groupedDialogue.DialogueName);
            }

            return groupedDialogueNames;
        }

        public List<string> GetUngroupedDialogueNames(bool startingDialoguesOnly)
        {
            List<string> ungroupedDialogueNames = new List<string>();

            foreach (Dialogue ungroupedDialogue in UngroupedDialogues)
            {
                if (startingDialoguesOnly && !ungroupedDialogue.IsStartingDialogue)
                {
                    continue;
                }

                ungroupedDialogueNames.Add(ungroupedDialogue.DialogueName);
            }

            return ungroupedDialogueNames;
        }
    }
}