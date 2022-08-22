using System;
using UnityEngine;

namespace DialogueSystem
{
    [Serializable]
    public class DialogueChoiceData
    {
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public Dialogue NextDialogue { get; set; }
    }
}