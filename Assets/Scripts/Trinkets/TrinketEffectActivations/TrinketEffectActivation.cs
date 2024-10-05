using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrinketEffectActivation : ScriptableObject
{
    protected GameObject _player;
    protected GameObject _activator;
    protected Bullet _bullet;
    protected GameObject _target;
    protected DamageEvent _dmgEvent;
    protected SpellInventoryData _spellInventoryData;
    protected TrinketInventoryData _trinketInventoryData;
    protected PlayerStats _playerStats;
    protected BulletLauncher _bulletLauncher;
    protected List<TrinketEffect> _previousEffectsInChain;

    public void ProvideContext(GameObject player, GameObject activator, Bullet bullet, GameObject target, DamageEvent dmgEvent, SpellInventoryData spellInv, TrinketInventoryData trinketInv, PlayerStats playerStats, BulletLauncher bulletLauncher, List<TrinketEffect> previousEffectsInChain)
    {
        _player = player;
        _activator = activator;
        _bullet = bullet;
        _target = target;
        _dmgEvent = dmgEvent;
        _spellInventoryData = spellInv;
        _trinketInventoryData = trinketInv;
        _playerStats = playerStats;
        _bulletLauncher = bulletLauncher;
        _previousEffectsInChain = previousEffectsInChain;
    }

    public abstract void Activate();
}