using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageTeam
{
    Enemy,
    Player,
    Neutral
}

public class DamageReceiver : MonoBehaviour
{
    public event Action<DamageReceiver, DamageEvent, DamageResult> OnDamage;

    [SerializeField]
    private DamageTeam _team;
    public DamageTeam Team => _team;

    public void ReceiveDamage(DamageEvent dmgEvent)
    {
        DamageResult result = HandleDamage(dmgEvent);

        OnDamage?.Invoke(this, dmgEvent, result);
    }

    protected virtual DamageResult HandleDamage(DamageEvent dmgEvent)
    {
        return new DamageResult(dmgEvent.Damage, true);
    }
}

public class DamageEvent
{
    public DamageEvent(float damage, GameObject mainSource, GameObject specificSource, DamageTypes damageType, bool armorPiercing)
    {
        Damage = damage;
        MainSource = mainSource;
        SpecificSource = specificSource;
        DamageType = damageType;
        ArmorPiercing = armorPiercing;
        AppliedDamage = damage;
    }

    public float Damage { get; }
    public GameObject MainSource { get; }
    public GameObject SpecificSource { get; }
    public DamageTypes DamageType { get; }
    public bool ArmorPiercing { get; }
    public float AppliedDamage { get; set; }
}

public class DamageResult
{
    public float DamageTaken { get; private set; }
    public bool Killed { get; private set; }

    public DamageResult(float damageTaken, bool killed)
    {
        DamageTaken = damageTaken;
        Killed = killed;
    }
}