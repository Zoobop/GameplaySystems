

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct DialogueOption
{
    public string optionText;
    public OldDialogue optionDialogue;
}

[CreateAssetMenu(menuName = "Dialogue/Dialogue", fileName = "Dialogue_")]
public class OldDialogue : ScriptableObject
{
    [TextArea(10, 30)]
    [SerializeField] private List<string> _preDialogue = new();
    [TextArea(10, 30)]
    [SerializeField] private string _dialogueText;
    [SerializeField] private List<DialogueOption> _dialogueOptions = new();
    [SerializeField] private bool _isCheckpoint;
    [SerializeField] private OldDialogue _checkpointDialogue;

    public IEnumerable<string> PreDialogue => _preDialogue;
    public string DialogueText => _dialogueText;
    public IEnumerable<DialogueOption> DialogueOptions => _dialogueOptions;
    public bool IsCheckpoint => _isCheckpoint;
    public OldDialogue CheckpointDialogue => _checkpointDialogue;
    
    public bool HasNext() => _dialogueOptions.Any();
}