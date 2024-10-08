using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class SummonManager : MonoBehaviour
{
    private List<Summon> _activeSummons = new List<Summon>();

    public void CreateSummon(Summon summon, PlayerStats playerStats, Vector3 position)
    {
        Summon s = Instantiate(summon, position, Quaternion.identity);
        _activeSummons.Add(s);
        s.Initialize(transform.root.gameObject, playerStats);
        s.OnLifetimeEnd += DestroySummon;
    }

    private void DestroySummon(Summon summon)
    {
        if (!_activeSummons.Contains(summon)) return;

        summon.OnLifetimeEnd -= DestroySummon;

        _activeSummons.Remove(summon);
        Destroy(summon.gameObject);
    }
}
