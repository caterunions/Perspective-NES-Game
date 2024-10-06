using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthDamageReceiver : DamageReceiver
{
    [SerializeField]
    private EntityStats _stats;

    [SerializeField]
    private List<HealthPool> _healthPools = new List<HealthPool>();
    public List<HealthPool> HealthPools => _healthPools;

    protected override DamageResult HandleDamage(DamageEvent dmgEvent)
    {
        float damage = dmgEvent.Damage;

        if (!dmgEvent.ArmorPiercing) damage *= (100 / (100 + _stats.Armor));

        dmgEvent.AppliedDamage = damage;

        while (damage > 0 && _healthPools.Where(hp => hp.Health > 0).Count() > 0)
        {
            damage -= _healthPools.First(hp => hp.Health > 0).Damage(damage);
        }

        return new DamageResult(dmgEvent.AppliedDamage, _healthPools[0].Health <= 0);
    }
}
