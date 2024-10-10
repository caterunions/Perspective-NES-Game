using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell Reward Table", menuName = "Scriptable Objects/Reward Tables/Spell Reward Table")]
public class SpellRewardTable : ScriptableObject
{
    [SerializeField]
    private WeightedSpell[] _weightedSpells;
    public WeightedSpell[] WeightedSpells => _weightedSpells;
}

[Serializable]
public class WeightedSpell
{
    [SerializeField]
    private Spell _spell;
    public Spell Spell => _spell;

    [SerializeField]
    private int _weight;
    public int Weight => _weight;
}