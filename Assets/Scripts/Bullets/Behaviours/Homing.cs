using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EntityRadar))]
public class Homing : BulletBehaviour
{
    private EntityRadar _radar;
    protected EntityRadar radar
    {
        get
        {
            if (_radar == null) _radar = GetComponent<EntityRadar>();
            return _radar;
        }
    }

    [SerializeField]
    private float _maxTurnDegrees;

    private void Update()
    {
        if (radar.TransformsInRadius.Count == 0) return;

        float lowestDist = Mathf.Infinity;
        Transform closest = radar.TransformsInRadius.First();
        foreach (Transform t in radar.TransformsInRadius)
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
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * _maxTurnDegrees);
    }
}
