using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Summon", menuName = "Scriptable Objects/Spell Effects/Create Summon")]
public class CreateSummon : SpellEffect
{
    [SerializeField]
    private Summon _summon;

    public override void Activate()
    {
        _summonManager.CreateSummon(_summon, _playerStats, _caster.transform.position);
    }
}
