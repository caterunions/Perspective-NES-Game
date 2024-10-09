using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TrinketRewards
{
    [SerializeField]
    private int _waveNumber;
    public int WaveNumber => _waveNumber;

    [SerializeField]
    private TrinketRewardTable _rewardTable;

    [SerializeField, Range(1, 8)]
    private int _optionsCount;

    public Trinket[] GetTrinketOptions()
    {
        Trinket[] options = new Trinket[_optionsCount];

        int[] weights = _rewardTable.WeightedTrinkets.Select(t => t.Weight).ToArray();
        for(int i = 0; i < _optionsCount; i++)
        {
            int randomWeight = UnityEngine.Random.Range(0, weights.Sum());
            for(int j = 0; j < weights.Length; j++)
            {
                randomWeight -= weights[j];
                if(randomWeight < 0)
                {
                    options[i] = _rewardTable.WeightedTrinkets[j].Trinket;
                    break;
                }
            }
        }

        return options;
    }
}
