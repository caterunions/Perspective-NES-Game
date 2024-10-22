using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Akaal.PvCustomizer.Scripts;

[CreateAssetMenu(fileName = "Trinket", menuName = "Scriptable Objects/Trinket")]
public class Trinket : ScriptableObject
{
    [SerializeField]
    [PvIcon]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField, TextArea(15, 20)]
    protected string _description;
    public string Description => _description;

    [SerializeField]
    private Rarity _rarity;
    public Rarity Rarity => _rarity;

    [SerializeField]
    private List<StatBoost> _statBoosts;
    public List<StatBoost> StatBoosts => _statBoosts;

    [SerializeField]
    private List<TrinketEffect> _trinketEffects;
    public List<TrinketEffect> TrinketEffects => _trinketEffects;
}