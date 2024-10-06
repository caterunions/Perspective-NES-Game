using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectProc : ScriptableObject
{
    protected GameObject _entity;
    protected EntityStats _stats;
    protected HealthDamageReceiver _healthDamageReceiver;
    protected StatusEffect _statusEffect;

    public void ProvideContext(GameObject entity, EntityStats stats, HealthDamageReceiver hdr, StatusEffect statusEffect)
    {
        _entity = entity;
        _stats = stats;
        _healthDamageReceiver = hdr;
        _statusEffect = statusEffect;
    }

    public abstract void Proc(int stacks);
}
