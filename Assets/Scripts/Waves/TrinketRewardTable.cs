using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trinket Reward Table", menuName = "Scriptable Objects/Reward Tables/Trinket Reward Table")]
public class TrinketRewardTable : ScriptableObject
{
    [SerializeField]
    private WeightedTrinket[] _weightedTrinkets;
    public WeightedTrinket[] WeightedTrinkets => _weightedTrinkets;
}

[Serializable]
public class WeightedTrinket
{
    [SerializeField]
    private Trinket _trinket;
    public Trinket Trinket => _trinket;

    [SerializeField]
    private int _weight;
    public int Weight => _weight;
}