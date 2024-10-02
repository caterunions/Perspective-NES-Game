using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelection : MonoBehaviour
{
    [SerializeField]
    private SpellInventoryData _spellInventoryData;

    [SerializeField]
    private CastingHandler _castingHandler;

    public void HandleScroll(float scroll)
    {
        if (_castingHandler.CastingSpell) return;

        if(scroll > 0)
        {
            if(_spellInventoryData.SelectedSpellIndex == _spellInventoryData.Spells.Length - 1)
            {
                _spellInventoryData.SelectedSpellIndex = 0;
            }
            else
            {
                _spellInventoryData.SelectedSpellIndex++;
            }
        }
        else if(scroll < 0)
        {
            if (_spellInventoryData.SelectedSpellIndex == 0)
            {
                _spellInventoryData.SelectedSpellIndex = _spellInventoryData.Spells.Length - 1;
            }
            else
            {
                _spellInventoryData.SelectedSpellIndex--;
            }
        }
    }
}
