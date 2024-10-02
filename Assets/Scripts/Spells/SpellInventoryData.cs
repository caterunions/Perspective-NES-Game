using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellInventory", menuName = "Scriptable Objects/SpellInventory")]
public class SpellInventoryData : ScriptableObject
{
    [SerializeField]
    private Spell _mainAttackSpell;
    public Spell MainAttackSpell => _mainAttackSpell;

    public Spell[] CastingSpells = new Spell[4];
}
