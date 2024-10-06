using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public StatusEffect(StatusEffectData statusEffectData, float endTime)
    {
        _statusEffectData = statusEffectData;
        _endTime = endTime;
        if (statusEffectData.TickRate > 0) _nextProc = statusEffectData.TickRate + Time.time;
        else _nextProc = float.MaxValue;
    }

    private StatusEffectData _statusEffectData;
    public StatusEffectData StatusEffectData => _statusEffectData;

    public List<StatBoost> LastAppliedBoosts { get; set; } 

    private float _endTime;
    public float EndTime
    {
        get { return _endTime; }
        set { _endTime = value; }
    }

    private float _nextProc;
    public float NextProc
    {
        get { return _nextProc; }
        set { _nextProc = value; }
    }

    private int _stackCount = 1;
    public int StackCount
    {
        get 
        {
            if (!_statusEffectData.Stackable) return 1;
            return _stackCount; 
        }
        set 
        { 
            _stackCount = value; 
        }
    }
}
