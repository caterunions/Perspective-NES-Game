using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrinketEffect
{
    [SerializeField]
    [ScriptableObjectId]
    private string _id;
    public string Id => _id;

    [SerializeField]
    private TrinketEffectTriggers _trigger;
    public TrinketEffectTriggers Trigger => _trigger;

    [Header("Only needed for the damage type triggers")]
    [SerializeField]
    private DamageTypes _damageType;
    public DamageTypes DamageType => _damageType;

    [SerializeField]
    private float _chance;
    public float Chance => _chance;

    [SerializeField]
    private List<TrinketEffectActivation> _activations;
    public List<TrinketEffectActivation> Activations => _activations;

    public void Proc(GameObject player, GameObject activator, Bullet bullet, GameObject target, DamageEvent dmgEvent, SpellInventoryData spellInv, TrinketInventoryData trinketInv, PlayerStats playerStats, BulletLauncher bulletLauncher, List<TrinketEffect> previousEffectsInChain)
    {
        List<TrinketEffect> effectsIncludingThis = previousEffectsInChain;
        effectsIncludingThis.Add(this);

        foreach (TrinketEffectActivation activation in _activations)
        {
            activation.ProvideContext(player, activator, bullet, target, dmgEvent, spellInv, trinketInv, playerStats, bulletLauncher, effectsIncludingThis);
            activation.Activate();
        }
    }

    public override bool Equals(System.Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            TrinketEffect ie = (TrinketEffect)obj;
            return (Id == ie.Id);
        }
    }
}

public enum TrinketEffectTriggers
{
    OnHit,
    OnKill,
    OnShoot,
    OnDamageTypeHit,
    OnDamageTypeKill,
    OnDamageTypeShoot,
    OnSummon,
    OnCastSpell,
    OnBuff,
    OnDebuff,
    OnTakeDamage,
    OnHeal
}
