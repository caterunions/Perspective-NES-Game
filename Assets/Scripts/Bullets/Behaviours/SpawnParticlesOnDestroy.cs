using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesOnDestroy : BulletBehaviour
{
    [SerializeField]
    private ParticleSystem _particles;

    private void OnDestroy()
    {
        Instantiate(_particles, transform.position, Quaternion.identity);
    }
}
