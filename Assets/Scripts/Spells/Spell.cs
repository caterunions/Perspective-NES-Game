using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    [SerializeField]
    private Sprite _icon;
    public Sprite Icon => _icon;

    [Header("As basic attack")]

    [SerializeField]
    private Attack _basicAttack;
    public Attack BasicAttack => _basicAttack;

    [Header("As mana-costing spell")]
    [SerializeField]
    private float _manaCost;
    public float ManaCost => _manaCost;

    [SerializeField]
    private float _cooldown;
    public float Cooldown => _cooldown;

    [Header("Only SpellStartEffects is required if not making a held spell.")]
    [SerializeField]
    private bool _canBeHeldDown;
    public bool CanBeHeldDown => _canBeHeldDown;

    [SerializeField]
    private float _manaDrain;
    public float ManaDrain => _manaDrain;

    [SerializeField]
    private List<SpellEffect> _spellStartEffects = new List<SpellEffect>();
    public List<SpellEffect> SpellStartEffects => _spellStartEffects;

    [SerializeField]
    private List<SpellSustainEffect> _spellSustainEffects = new List<SpellSustainEffect>();
    public List<SpellSustainEffect> SpellSustainEffects => _spellSustainEffects;

    [SerializeField]
    private List<SpellEffect> _spellEndEffects = new List<SpellEffect>();
    public List<SpellEffect> SpellEndEffects => _spellEndEffects;
}
