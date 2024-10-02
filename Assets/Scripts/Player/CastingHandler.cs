using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingHandler : MonoBehaviour
{
    private Spell _curSpell;

    [SerializeField]
    private BulletLauncher _bulletLauncher;

    [SerializeField]
    private PlayerStats _stats;

    public SpellInventoryData SpellInventoryData { get; set; }

    [SerializeField]
    private GameObject _player;

    private bool _castingSpell = false;
    public bool CastingSpell => _castingSpell;

    private float _curSpellCooldown = 0f;
    public float CurSpellCooldown => _curSpellCooldown;

    private float _maxSpellCooldown = 0f;
    public float MaxSpellCooldown => _maxSpellCooldown;

    public void StartCast()
    {
        if (_curSpell == null) return;
        if (_castingSpell) return;
        if (_curSpellCooldown > 0) return;
        if (_stats.CurrentMana < _curSpell.ManaCost) return;

        _castingSpell = true;
        _curSpellCooldown = _curSpell.Cooldown;
        _maxSpellCooldown = _curSpell.Cooldown;
        _stats.LoseMana(_curSpell.ManaCost);

        foreach(SpellEffect effect in _curSpell.SpellStartEffects)
        {
            effect.ProvideContext(_player, SpellInventoryData, _stats, _bulletLauncher);
            effect.Activate();
        }

        if(_curSpell.CanBeHeldDown)
        {
            foreach(SpellSustainEffect susEffect in _curSpell.SpellSustainEffects)
            {
                susEffect.ProvideContext(_player, SpellInventoryData, _stats, _bulletLauncher);
                susEffect.Activate();
            }
        } 
        else StopCast();
    }

    public void StopCast()
    {
        if (_curSpell == null) return;
        if (!_castingSpell) return;

        _castingSpell = false;

        if(_curSpell.CanBeHeldDown)
        {
            foreach(SpellSustainEffect susEffect in _curSpell.SpellSustainEffects)
            {
                susEffect.Cancel();
            }
            foreach(SpellEffect effect in _curSpell.SpellEndEffects)
            {
                effect.ProvideContext(_player, SpellInventoryData, _stats, _bulletLauncher);
                effect.Activate();
            }
        }
    }

    public void ChangeSpell(Spell spell)
    {
        _curSpell = spell;
        StopCast();
    }

    private void Update()
    {
        if(!_castingSpell && _curSpellCooldown > 0)
        {
            _curSpellCooldown -= Time.deltaTime;
            if (_curSpellCooldown < 0) _curSpellCooldown = 0;
        }
        if(_castingSpell && _curSpell.CanBeHeldDown)
        {
            if (_curSpell.ManaDrain * Time.deltaTime > _stats.CurrentMana) StopCast();
            else _stats.LoseMana(_curSpell.ManaDrain * Time.deltaTime);
        }
    }
}
