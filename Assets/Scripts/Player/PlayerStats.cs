using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    public event Action<PlayerStats, float> OnManaChange;

    [SerializeField]
    private float _maxMana;
    public float MaxMana => _maxMana;

    [SerializeField]
    private float _manaRegen;
    public float ManaRegen => _manaRegen;

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
}
