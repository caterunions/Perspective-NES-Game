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

    private void OnEnable()
    {
        _spellInventoryData.OnSpellSelectionChange += HandleSpellChange;
        HandleSpellChange(_spellInventoryData);

        _castingHandler.SpellInventoryData = _spellInventoryData;
    }

    private void OnDisable()
    {
        _spellInventoryData.OnSpellSelectionChange -= HandleSpellChange;
    }

    private void HandleSpellChange(SpellInventoryData spelldata)
    {
        _castingHandler.ChangeSpell(_spellInventoryData.SelectedSpell);
        _playerFire.ChangeSpell(_spellInventoryData.SelectedSpell);
    }
}
