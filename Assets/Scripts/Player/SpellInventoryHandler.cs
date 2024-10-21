using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventoryHandler : MonoBehaviour
{
    [SerializeField]
    private SpellInventoryData _spellInventoryData;

    [SerializeField]
    private PlayerFire _playerFire;

    [SerializeField]
    private CastingHandler _castingHandler;

    [SerializeField]
    private Spell[] _startingSpells = new Spell[5];

    private void OnEnable()
    {
        _spellInventoryData.SetSpells(_startingSpells);

        _spellInventoryData.OnSpellSelectionChange += HandleSpellSelectionChange;
        _spellInventoryData.OnSpellsChanged += HandleSpellsChange;

        HandleSpellSelectionChange(_spellInventoryData);

        _castingHandler.SpellInventoryData = _spellInventoryData;
    }

    private void OnDisable()
    {
        _spellInventoryData.OnSpellSelectionChange -= HandleSpellSelectionChange;
        _spellInventoryData.OnSpellsChanged -= HandleSpellsChange;
    }

    private void HandleSpellSelectionChange(SpellInventoryData spellInv)
    {
        _castingHandler.ChangeSpell(_spellInventoryData.SelectedSpell);
        _playerFire.ChangeSpell(_spellInventoryData.SelectedSpell);
    }

    private void HandleSpellsChange(SpellInventoryData spellInv, Spell[] oldSpells)
    {
        _castingHandler.ChangeSpell(_spellInventoryData.SelectedSpell);
        _playerFire.ChangeSpell(_spellInventoryData.SelectedSpell);
    }
}
