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
    public bool a;

    [Header("As tower ability")]
    public bool b;
}
