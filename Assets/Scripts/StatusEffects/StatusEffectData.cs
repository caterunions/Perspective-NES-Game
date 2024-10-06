using Akaal.PvCustomizer.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status Effect Data", menuName = "Scriptable Objects/Status Effect Data")]
public class StatusEffectData : ScriptableObject
{
    [SerializeField]
    [PvIcon]
    private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField]
    private List<StatBoost> _statBoosts;
    public List<StatBoost> StatBoosts => _statBoosts;

    [SerializeField]
    private bool _stackable;
    public bool Stackable => _stackable;

    [SerializeField]
    private StackTypes _stackType;
    public StackTypes StackType => _stackType;

    [SerializeField]
    private float _tickRate;
    public float TickRate => _tickRate;

    [SerializeField]
    private List<StatusEffectProc> _procs;
    public List<StatusEffectProc> Procs => _procs;
}

public enum StackTypes
{
    Linear,
    Hyperbolic,
    Exponential
}
