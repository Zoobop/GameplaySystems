using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    using Utility.ExtensionMethods;
    
    public class DialogueArchive : MonoBehaviour, IDialogueArchive
    {
        [Header("Events")]
        [SerializeField] private UnityEvent _onDialogueStart;
        [SerializeField] private UnityEvent _onDialogueEnd;
        
        /* Dialogue Scriptable Objects */
        [SerializeField] private DialogueContainer dialogueContainer;
        [SerializeField] private DialogueGroup dialogueGroup;
        [SerializeField] private Dialogue dialogue;

        /* Filters */
        [SerializeField] private bool groupedDialogues;
        [SerializeField] private bool startingDialoguesOnly;

        /* Indexes */
        [SerializeField] private int selectedDialogueGroupIndex;
        [SerializeField] private int selectedDialogueIndex;

        private readonly IDictionary<Dialogue, DialogueRequirement> _requirements = new Dictionary<Dialogue, DialogueRequirement>();

        #region UnityEvents

        private void Awake()
        {
            _requirements.Clear();
            FindRequirements();
        }

        private void OnValidate()
        {
            _requirements.Clear();
            FindRequirements();
        }

        #endregion
        
        #region IDialogueArchive

        public bool HasDialogue()
        {
            return dialogue != null;
        }
        
        public Dialogue GetCurrentDialogue()
        {
            //dialogueContainer.DialogueGroups[dialogueGroup][selectedDialogueIndex];
            return dialogue;
        }

        public IDictionary<Dialogue, DialogueRequirement> GetRequirements()
        {
            return _requirements;
        }

        public void OnDialogueStart()
        {
            _onDialogueStart?.Invoke();
        }

        public void OnDialogueEnd()
        {
            _onDialogueEnd?.Invoke();
        }
        
        #endregion
        
        public void AddStartEvent(UnityAction action)
        {
            _onDialogueStart.AddListener(action);
        }
    
        public void RemoveStartEvent(UnityAction action)
        {
            _onDialogueStart.RemoveListener(action);
        }
    
        public void AddEndEvent(UnityAction action)
        {
            _onDialogueEnd.AddListener(action);
        }
    
        public void RemoveEndEvent(UnityAction action)
        {
            _onDialogueEnd.RemoveListener(action);
        }

        private void FindRequirements()
        {
            var requirements = GetComponents<DialogueRequirement>();
            if (requirements == null || requirements.IsEmpty()) return;
            
            foreach (var dialogueRequirement in requirements)
            {
                if (dialogueRequirement == null || dialogueRequirement.DialogueToCheck == null) continue;
                _requirements.Add(dialogueRequirement.DialogueToCheck, dialogueRequirement);
            }
        }
    }
}