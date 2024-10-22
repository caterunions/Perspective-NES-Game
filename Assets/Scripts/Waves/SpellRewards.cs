using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SpellRewards
{
    [SerializeField]
    private int _waveNumber;
    public int WaveNumber => _waveNumber;

    [SerializeField]
    private List<Spell> _rewardSpells;
    public List<Spell> RewardSpells => _rewardSpells;

    [SerializeField]
    private SpellRewardTable _rewardTable;

    [SerializeField, Range(1, 8)]
    private int _optionsCount;

    public Spell[] GetSpellOptions()
    {
        Spell[] options = new Spell[_optionsCount];

        int[] weights = _rewardTable.WeightedSpells.Select(s => s.Weight).ToArray();
        for (int i = 0; i < _optionsCount; i++)
        {
            int randomWeight = UnityEngine.Random.Range(0, weights.Sum());
            for (int j = 0; j < weights.Length; j++)
            {
                randomWeight -= weights[j];
                if (randomWeight < 0)
                {
                    options[i] = _rewardTable.WeightedSpells[j].Spell;
                    break;
                }
            }
        }

        return options;
    }
}
