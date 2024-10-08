using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : ScriptableObject
{
    protected GameObject _caster;
    protected SpellInventoryData _spellInventoryData;
    protected PlayerStats _playerStats;
    protected BulletLauncher _bulletLauncher;
    protected SummonManager _summonManager;

    public void ProvideContext(GameObject caster, SpellInventoryData spellData, PlayerStats stats, BulletLauncher launcher, SummonManager manager)
    {
        _caster = caster;
        _spellInventoryData = spellData;
        _playerStats = stats;
        _bulletLauncher = launcher;
        _summonManager = manager;
    }

    public abstract void Activate();
}
