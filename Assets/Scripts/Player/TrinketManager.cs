using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrinketManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private BulletLauncher _bulletLauncher;

    [SerializeField]
    private PlayerStats _playerStats;

    [SerializeField]
    private SpellInventoryData _spellInv;

    [SerializeField]
    private TrinketInventoryData _trinketInv;

    private List<TrinketEffect> _activeTrinketEffects = new List<TrinketEffect>();

    private void OnEnable()
    {
        _trinketInv.OnTrinketsChange += HandleTrinketsChange;

        _bulletLauncher.OnSpawnedBulletHit += HandleOnHitEffects;
        _bulletLauncher.OnBulletLaunch += HandleOnShootEffects;

        HandleTrinketsChange(_trinketInv, new List<Trinket>());
    }

    private void OnDisable()
    {
        _trinketInv.OnTrinketsChange -= HandleTrinketsChange;

        _bulletLauncher.OnSpawnedBulletHit -= HandleOnHitEffects;
        _bulletLauncher.OnBulletLaunch -= HandleOnShootEffects;

        foreach (StatBoost statBoost in _playerStats.ActiveStatBoosts.ToList())
        {
            if (statBoost.Source == StatBoostSource.Trinket) _playerStats.ActiveStatBoosts.Remove(statBoost);
        }
    }

    private void HandleTrinketsChange(TrinketInventoryData trinketInv, List<Trinket> oldTrinkets)
    {
        foreach (Trinket trinket in oldTrinkets)
        {
            if (trinket != null)
            {
                foreach (StatBoost statBoost in trinket.StatBoosts)
                {
                    if (_playerStats.ActiveStatBoosts.Any(sb => sb.Equals(statBoost))) _playerStats.ActiveStatBoosts.Remove(_playerStats.ActiveStatBoosts.First(sb => sb.Equals(statBoost)));
                }
                foreach (TrinketEffect trinketEffect in trinket.TrinketEffects)
                {
                    if (_activeTrinketEffects.Any(ie => ie.Equals(trinketEffect))) _activeTrinketEffects.Remove(_activeTrinketEffects.First(ie => ie.Equals(trinketEffect)));
                }
            }
        }
        foreach (Trinket trinket in _trinketInv.Trinkets)
        {
            if (trinket != null)
            {
                foreach (StatBoost statBoost in trinket.StatBoosts)
                {
                    _playerStats.ActiveStatBoosts.Add(statBoost);
                }
                foreach (TrinketEffect trinketEffect in trinket.TrinketEffects)
                {
                    _activeTrinketEffects.Add(trinketEffect);
                }
            }
        }
    }

    private void ProcTrinketEffects(TrinketEffectTriggers trigger, DamageTypes damageType, GameObject activator, Bullet bullet, GameObject target, DamageEvent dmgEvent)
    {
        foreach (TrinketEffect trinketEffect in _activeTrinketEffects.Where(te => te.Trigger == trigger))
        {
            if (bullet != null && (
                trigger == TrinketEffectTriggers.OnDamageTypeHit ||
                trigger == TrinketEffectTriggers.OnDamageTypeKill ||
                trigger == TrinketEffectTriggers.OnDamageTypeShoot))
            {
                if (trinketEffect.DamageType != damageType) return;
            }

            float chance = trinketEffect.Chance;
            List<TrinketEffect> trinketEffectsInChain = new List<TrinketEffect>();

            if (bullet != null)
            {
                if (bullet.PreviousEffectsInChain.Any(te => te.Equals(trinketEffect))) return;

                chance *= bullet.ProCoefficient;
                trinketEffectsInChain = bullet.PreviousEffectsInChain;
            }

            if (UnityEngine.Random.Range(0, 100) <= chance)
            {
                trinketEffect.Proc(_player, activator, bullet, target, dmgEvent, _spellInv, _trinketInv, _playerStats, _bulletLauncher, trinketEffectsInChain);
            }
        }
    }

    private void HandleOnHitEffects(BulletLauncher launcher, Bullet bullet, DamageReceiver dr, DamageEvent dmgEvent)
    {
        ProcTrinketEffects(TrinketEffectTriggers.OnHit, bullet.DamageType, _player, bullet, dr.gameObject, dmgEvent);
        ProcTrinketEffects(TrinketEffectTriggers.OnDamageTypeHit, bullet.DamageType, _player, bullet, dr.gameObject, dmgEvent);

        HealthDamageReceiver hdr = dr as HealthDamageReceiver;
        if (hdr != null && hdr.HealthPools.All(pool => pool.Health <= 0))
        {
            ProcTrinketEffects(TrinketEffectTriggers.OnKill, bullet.DamageType, _player, bullet, dr.gameObject, dmgEvent);
            ProcTrinketEffects(TrinketEffectTriggers.OnDamageTypeKill, bullet.DamageType, _player, bullet, dr.gameObject, dmgEvent);
        }
    }

    private void HandleOnShootEffects(BulletLauncher launcher, Bullet bullet, PatternData pattern)
    {
        if (bullet.CameFromEffect) return;

        ProcTrinketEffects(TrinketEffectTriggers.OnShoot, bullet.DamageType, _player, bullet, null, null);
        ProcTrinketEffects(TrinketEffectTriggers.OnDamageTypeShoot, bullet.DamageType, _player, bullet, null, null);
    }
}
