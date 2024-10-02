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

    private void Update()
    {
        if(stats.CurrentMana < stats.MaxMana)
        {
            stats.GainMana(stats.ManaRegen * Time.deltaTime);
        }
    }
}
