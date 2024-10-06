using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatusEffect : BulletBehaviour
{
    [SerializeField]
    private StatusEffectData _statusEffectData;

    [SerializeField]
    private float _duration;

    private void OnEnable()
    {
        bullet.OnHit += ApplyStatus;
    }

    private void OnDisable()
    {
        bullet.OnHit -= ApplyStatus;
    }

    private void ApplyStatus(Bullet bullet, DamageReceiver dr, DamageEvent dmgEvent)
    {
        StatusEffectManager manager = dr.GetComponentInParent<StatusEffectManager>();
        if (manager == null) return;

        manager.AddStatusEffect(new StatusEffect(_statusEffectData, Time.time + _duration));
    }
}
