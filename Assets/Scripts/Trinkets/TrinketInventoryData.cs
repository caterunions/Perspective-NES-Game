using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trinket Inventory", menuName = "Scriptable Objects/Trinket Inventory")]
public class TrinketInventoryData : ScriptableObject
{
    public event Action<TrinketInventoryData, List<Trinket>> OnTrinketsChange;
    public event Action<TrinketInventoryData, Trinket> OnTrinketAdd;

    [SerializeField]
    private List<Trinket> _trinkets;
    public List<Trinket> Trinkets => _trinkets;

    public void AddTrinket(Trinket trinket)
    {
        List<Trinket> _oldTrinkets = new List<Trinket>(_trinkets);
        _trinkets.Add(trinket);
        OnTrinketAdd?.Invoke(this, trinket);
        OnTrinketsChange?.Invoke(this, _oldTrinkets);
    }

    public void ClearTrinkets()
    {
        _trinkets.Clear();
    }
}
