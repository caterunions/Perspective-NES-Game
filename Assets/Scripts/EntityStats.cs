using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField]
    protected float _maxHealth;
    public float MaxHealth
    {
        get { return ApplyStatBoosts(_maxHealth, StatTypes.MaxHealth); }
    }

    [SerializeField]
    protected float _regen;
    public float Regen
    {
        get { return ApplyStatBoosts(_regen, StatTypes.Regen); }
    }

    protected float _damageMultiplier = 1;
    public float DamageMultiplier
    {
        get { return ApplyStatBoosts(_damageMultiplier, StatTypes.GlobalDamageMult); }
    }

    protected float _attackSpeed = 1;
    public float AttackSpeed
    {
        get { return ApplyStatBoosts(_attackSpeed, StatTypes.AttackSpeed); }
    }

    [SerializeField]
    protected float _armor;
    public float Armor
    {
        get { return ApplyStatBoosts(_armor, StatTypes.Armor); }
    }

    [SerializeField]
    protected float _moveSpeed;
    public float MoveSpeed
    {
        get { return ApplyStatBoosts(_moveSpeed, StatTypes.MoveSpeed); }
    }

    protected List<StatBoost> _activeStatBoosts = new List<StatBoost>();
    public List<StatBoost> ActiveStatBoosts => _activeStatBoosts;

    protected float ApplyStatBoosts(float baseValue, StatTypes statType)
    {
        float modifiedStat = baseValue;

        List<StatBoost> applicableBoosts = ActiveStatBoosts.Where(sb => sb.StatIncrease == statType).ToList();
        float finalMultiplier = 1;

        foreach (StatBoost boost in applicableBoosts)
        {
            if (boost.Type == StatBoostType.Additive)
            {
                modifiedStat += boost.StatIncreaseAmount;
            }
            else if (boost.Type == StatBoostType.Multiplicative)
            {
                finalMultiplier += boost.StatIncreaseAmount;
            }
        }

        modifiedStat *= finalMultiplier;

        return modifiedStat;
    }
}
