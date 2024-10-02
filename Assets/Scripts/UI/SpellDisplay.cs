using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDisplay : MonoBehaviour
{
    [SerializeField]
    private SpellInventoryData _spellInventoryData;

    [SerializeField]
    private Image[] _spellIcons = new Image[6];

    [SerializeField]
    private Sprite _emptySprite;

    private void OnEnable()
    {
        _spellInventoryData.OnMainAttackChanged += HandleMainAttackChange;
        HandleMainAttackChange(_spellInventoryData, null);

        _spellInventoryData.OnCastingSpellsChanged += HandleCastingSpellsChange;
        HandleCastingSpellsChange(_spellInventoryData, new Spell[_spellInventoryData.CastingSpells.Length]);
    }

    private void OnDisable()
    {
        _spellInventoryData.OnMainAttackChanged -= HandleMainAttackChange;

        _spellInventoryData.OnCastingSpellsChanged -= HandleCastingSpellsChange;
    }

    private void HandleMainAttackChange(SpellInventoryData spellData, Spell oldSpell)
    {
        if(_spellInventoryData.MainAttackSpell != null)
        {
            _spellIcons[0].sprite = _spellInventoryData.MainAttackSpell.Icon;
        }
        else
        {
            _spellIcons[0].sprite = _emptySprite;
        }
    }

    private void HandleCastingSpellsChange(SpellInventoryData spellData, Spell[] oldCastingSpells)
    {
        for(int i = 0; i < _spellInventoryData.CastingSpells.Length; i++)
        {
            if(_spellInventoryData.CastingSpells[i] != null)
            {
                _spellIcons[i + 1].sprite = _spellInventoryData.CastingSpells[i].Icon;
            }
            else
            {
                _spellIcons[i + 1].sprite = _emptySprite;
            }
        }
    }
}
