using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellInventory", menuName = "Scriptable Objects/SpellInventory")]
public class SpellInventoryData : ScriptableObject
{
    public event Action<SpellInventoryData, Spell[]> OnSpellsChanged;
    public event Action<SpellInventoryData> OnSpellSelectionChange;

    [SerializeField]
    private Spell[] _spells = new Spell[5];
    public Spell[] Spells => _spells;

    private int _selectedSpellIndex = 0;
    public int SelectedSpellIndex
    {
        get { return _selectedSpellIndex; }
        set 
        {
            _selectedSpellIndex = value;
            OnSpellSelectionChange?.Invoke(this);
        }
    }

    public Spell SelectedSpell
    {
        get { return _spells[_selectedSpellIndex]; }
    }

    public void SetCastingSpellSlot(int index, Spell spell)
    {
        Spell[] old = new Spell[_spells.Length];
        Array.Copy(_spells, old, _spells.Length);
        _spells[index] = spell;
        OnSpellsChanged?.Invoke(this, old);
    }


}
