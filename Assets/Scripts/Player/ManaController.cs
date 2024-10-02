using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    private PlayerStats _stats;
    private PlayerStats stats
    {
        get
        {
            if (_stats == null) _stats = GetComponentInParent<PlayerStats>();
            return _stats;
        }
    }

    [SerializeField]
    private CastingHandler _castingHandler;

    private void Update()
    {
        if(!_castingHandler.CastingSpell && stats.CurrentMana < stats.MaxMana)
        {
            stats.GainMana(stats.ManaRegen * Time.deltaTime);
        }
    }
}
