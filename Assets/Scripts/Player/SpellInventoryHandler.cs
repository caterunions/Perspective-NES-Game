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
        _spellInventoryData.OnMainAttackChanged += HandleMainAttackChange;
        HandleMainAttackChange(_spellInventoryData, null);

        _spellInventoryData.OnSpellSelectionChange += HandleCastingSpellChange;
        HandleCastingSpellChange(_spellInventoryData);

        _castingHandler.SpellInventoryData = _spellInventoryData;
    }

    private void OnDisable()
    {
        _spellInventoryData.OnMainAttackChanged -= HandleMainAttackChange;

        _spellInventoryData.OnSpellSelectionChange -= HandleCastingSpellChange;
    }

    private void HandleMainAttackChange(SpellInventoryData spellData, Spell oldSpell)
    {
        _playerFire.ChangeMainAttackSpell(_spellInventoryData.MainAttackSpell);
    }

    private void HandleCastingSpellChange(SpellInventoryData spelldata)
    {
        _castingHandler.ChangeSpell(_spellInventoryData.SelectedCastingSpell);
    }
}
