using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public abstract class Skill : ScriptableObject
{
    [Header("General Information")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [TextArea]
    [SerializeField] private string _description;

    [SerializeField, Min(0)] private int _cost;

    [Header("Effects")]
    [SerializeField] private UnityEvent _events;

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public UnityEvent Events => _events;
}
