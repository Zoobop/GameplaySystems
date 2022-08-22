using System;
using UnityEngine;

namespace DialogueSystem.Editor
{
    [Serializable]
    public class DSChoiceSaveData
    {
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public string NodeID { get; set; }
    }
}