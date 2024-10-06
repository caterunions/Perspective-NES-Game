using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Entity", menuName = "Scriptable Objects/Status Effect Procs/Damage Entity")]
public class DamageEntityProc : StatusEffectProc
{
    [SerializeField]
    private float _damage;

    [SerializeField]
    private DamageTypes _damageType;

    [SerializeField]
    private StackTypes _stackType;

    [SerializeField]
    private bool _ignoreArmor;

    public override void Proc(int stacks)
    {
        float scaledDamage = 0;

        Debug.Log(stacks);

        switch (_stackType)
        {
            case StackTypes.Linear:
                scaledDamage = _damage * stacks;
                break;
            case StackTypes.Hyperbolic:
                scaledDamage = 1 - 1 / (1 + (_damage * stacks));
                break;
            case StackTypes.Exponential:
                scaledDamage = Mathf.Pow(1 + _damage, stacks);
                break;
        }

        Debug.Log(scaledDamage);

        _healthDamageReceiver.ReceiveDamage(new DamageEvent(scaledDamage, null, null, _damageType, _ignoreArmor));
    }
}
