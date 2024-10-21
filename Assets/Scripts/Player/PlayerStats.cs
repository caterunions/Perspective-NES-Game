using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    public event Action<PlayerStats, float> OnManaChange;

    [SerializeField]
    private PlayerFire _playerFire;

    [SerializeField]
    private CastingHandler _castingHandler;

    [SerializeField]
    private float _moveSpeedMultWhenFiring = 0.5f;

    public float CurrentMoveSpeed
    {
        get 
        {
            if (_playerFire.Firing || _castingHandler.CastingSpell) return MoveSpeed * _moveSpeedMultWhenFiring;
            return MoveSpeed;
        }
    }

    // mana
    [SerializeField]
    private float _maxMana;
    public float MaxMana
    {
        get { return ApplyStatBoosts(_maxMana, StatTypes.MaxMana); }
    }

    [SerializeField]
    private float _manaRegen;
    public float ManaRegen
    {
        get { return ApplyStatBoosts(_manaRegen, StatTypes.ManaRegen); }
    }

    private float _currentMana;
    public float CurrentMana => _currentMana;

    public void GainMana(float mana)
    {
        if (mana < 0) return;

        _currentMana += mana;
        if(_currentMana > _maxMana) _currentMana = _maxMana;

        OnManaChange?.Invoke(this, mana);
    }

    public void LoseMana(float mana)
    {
        if (mana < 0) return;

        _currentMana -= mana;
        if (_currentMana < 0) _currentMana = 0;

        OnManaChange?.Invoke(this, -mana);
    }

    // elemental damage boosts

    private float _arcaneDamageMultipler = 1;
    public float ArcaneDamageMultipler
    {
        get { return ApplyStatBoosts(_arcaneDamageMultipler, StatTypes.ArcaneDamageMult) + (DamageMultiplier - 1); }
    }

    private float _fireDamageMultiplier = 1;
    public float FireDamageMultiplier
    {
        get { return ApplyStatBoosts(_fireDamageMultiplier, StatTypes.FireDamageMult) + (DamageMultiplier - 1); }
    }

    private float _iceDamageMultiplier = 1;
    public float IceDamageMultiplier
    {
        get { return ApplyStatBoosts(_iceDamageMultiplier, StatTypes.IceDamageMult) + (DamageMultiplier - 1); }
    }

    private float _electricDamageMultiplier = 1;
    public float ElectricDamageMultiplier
    {
        get { return ApplyStatBoosts(_electricDamageMultiplier, StatTypes.ElectricDamageMult) + (DamageMultiplier - 1); }
    }

    private float _poisonDamageMultiplier = 1;
    public float PoisonDamageMultiplier
    {
        get { return ApplyStatBoosts(_poisonDamageMultiplier, StatTypes.PoisonDamageMult) + (DamageMultiplier - 1); }
    }

    private float _bloodDamageMultiplier = 1;
    public float BloodDamageMultiplier
    {
        get { return ApplyStatBoosts(_bloodDamageMultiplier, StatTypes.BloodDamageMult) + (DamageMultiplier - 1); }
    }

    private float _summonLifetimeMultiplier = 1;
    public float SummonLifetimeMultiplier
    {
        get { return ApplyStatBoosts(_summonLifetimeMultiplier, StatTypes.SummonLifetimeMult); }
    }

    private float _summonAttackSpeed = 1;
    public float SummonAttackSpeed
    {
        get { return ApplyStatBoosts(_summonAttackSpeed, StatTypes.SummonAttackSpeed); }
    }

    private float _summonDamageMultiplier = 1;
    public float SummonDamageMultiplier
    {
        get { return ApplyStatBoosts(_summonDamageMultiplier, StatTypes.SummonDamageMult); }
    }
}
