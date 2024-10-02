using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : ScriptableObject
{
    protected GameObject _caster;
    protected SpellInventoryData _spellInventoryData;
    protected PlayerStats _playerStats;
    protected BulletLauncher _bulletLauncher;

    public void ProvideContext(GameObject caster, SpellInventoryData spellData, PlayerStats stats, BulletLauncher launcher)
    {
        _caster = caster;
        _spellInventoryData = spellData;
        _playerStats = stats;
        _bulletLauncher = launcher;
    }

    public abstract void Activate();
}
