using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDisplay : MonoBehaviour
{
    [SerializeField]
    private SpellInventoryData _spellInventoryData;

    [SerializeField]
    private SpellIcon[] _spellIcons = new SpellIcon[5];

    [SerializeField]
    private RectTransform _spellSelection;

    [SerializeField]
    private Sprite _emptySprite;

    private void OnEnable()
    {
        _spellInventoryData.OnSpellsChanged += HandleCastingSpellsChange;
        HandleCastingSpellsChange(_spellInventoryData, new Spell[_spellInventoryData.Spells.Length]);

        _spellInventoryData.OnSpellSelectionChange += HandleSpellSelectionChange;
    }

    private void OnDisable()
    {
        _spellInventoryData.OnSpellsChanged -= HandleCastingSpellsChange;

        _spellInventoryData.OnSpellSelectionChange -= HandleSpellSelectionChange;
    }

    private void Start()
    {
        HandleCastingSpellsChange(_spellInventoryData, new Spell[_spellInventoryData.Spells.Length]);
    }

    private void HandleCastingSpellsChange(SpellInventoryData spellData, Spell[] oldCastingSpells)
    {
        for(int i = 0; i < _spellInventoryData.Spells.Length; i++)
        {
            if(_spellInventoryData.Spells[i] != null)
            {
                _spellIcons[i].SetIcon(_spellInventoryData.Spells[i].Icon);
            }
            else
            {
                _spellIcons[i].SetIcon(_emptySprite);
            }
        }
    }

    private void HandleSpellSelectionChange(SpellInventoryData spellData)
    {
        _spellSelection.transform.position = _spellIcons[_spellInventoryData.SelectedSpellIndex].transform.position;
    }
}
