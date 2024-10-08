using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SummonAim : MonoBehaviour
{
    private Summon _summon;
    protected Summon Summon
    {
        get
        {
            if (_summon == null) _summon = GetComponentInParent<Summon>();
            return _summon;
        }
    }

    private void Update()
    {
        if (Summon.Radar.TransformsInRadius.Count == 0) return;

        float lowestDist = Mathf.Infinity;
        Transform closest = Summon.Radar.TransformsInRadius.First();
        foreach (Transform t in Summon.Radar.TransformsInRadius)
        {
            float dist = Vector2.Distance(transform.position, t.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                closest = t;
            }
        }

        Vector3 dir = closest.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = rot;
    }
}
