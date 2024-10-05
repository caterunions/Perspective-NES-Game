using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatBoost
{
    [SerializeField]
    private StatTypes _statIncrease;
    public StatTypes StatIncrease => _statIncrease;

    [SerializeField]
    private StatBoostType _type;
    public StatBoostType Type => _type;

    [SerializeField]
    private StatBoostSource _source;
    public StatBoostSource Source => _source;

    [SerializeField]
    private float _statIncreaseAmount;
    public float StatIncreaseAmount => _statIncreaseAmount;

    public override bool Equals(System.Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            StatBoost sb = (StatBoost)obj;
            return (_statIncrease == sb.StatIncrease) && (_type == sb.Type) && (_source == sb.Source) && (_statIncreaseAmount == sb.StatIncreaseAmount);
        }
    }
}

public enum StatTypes
{
    // entity stat types
    MaxHealth,
    Regen,
    GlobalDamageMult,
    AttackSpeed,
    Armor,
    MoveSpeed,

    // player stat types
    ArcaneDamageMult,
    FireDamageMult,
    IceDamageMult,
    ElectricDamageMult,
    PoisonDamageMult,
    BloodDamageMult,
    MaxMana,
    ManaRegen
}

public enum StatBoostType
{
    Additive,
    Multiplicative
}

public enum StatBoostSource
{
    Trinket,
    StatusEffect
}