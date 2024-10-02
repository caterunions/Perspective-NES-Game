using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellInventory", menuName = "Scriptable Objects/SpellInventory")]
public class SpellInventoryData : ScriptableObject
{
    public event Action<SpellInventoryData, Spell> OnMainAttackChanged;
    public event Action<SpellInventoryData, Spell[]> OnCastingSpellsChanged;

    [SerializeField]
    private Spell _mainAttackSpell;
    public Spell MainAttackSpell => _mainAttackSpell;

    [SerializeField]
    private Spell[] _castingSpells = new Spell[5];
    public Spell[] CastingSpells => _castingSpells;

    public void SetMainAttackSpell(Spell spell)
    {
        Spell old = _mainAttackSpell;
        _mainAttackSpell = spell;
        OnMainAttackChanged?.Invoke(this, old);
    }

    public void SetCastingSpellSlot(int index, Spell spell)
    {
        Spell[] old = new Spell[_castingSpells.Length];
        Array.Copy(_castingSpells, old, _castingSpells.Length);
        _castingSpells[index] = spell;
        OnCastingSpellsChanged?.Invoke(this, old);
    }
}
