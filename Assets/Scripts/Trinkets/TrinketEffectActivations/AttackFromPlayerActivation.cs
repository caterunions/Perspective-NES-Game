using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(fileName = "Attack From Player", menuName = "Scriptable Objects/Trinket Effect Activations/Attack From Player")]
public class AttackFromPlayerActivation : TrinketEffectActivation
{
    [SerializeField]
    private Attack _attack;

    private Coroutine _attackRoutine;

    public override void Activate()
    {
        _attackRoutine = CoroutineHelper.Instance.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        int curCount = _attack.Count;
        float curSpread = _attack.Spread;
        float curAngleOffset = _attack.AngleOffsetStart;

        float damageMultiplier = 1;

        switch (_attack.Bullet.DamageType)
        {
            case DamageTypes.Arcane:
                damageMultiplier = _playerStats.ArcaneDamageMultipler; break;
            case DamageTypes.Fire:
                damageMultiplier = _playerStats.FireDamageMultiplier; break;
            case DamageTypes.Ice:
                damageMultiplier = _playerStats.IceDamageMultiplier; break;
            case DamageTypes.Electric:
                damageMultiplier = _playerStats.ElectricDamageMultiplier; break;
            case DamageTypes.Poison:
                damageMultiplier = _playerStats.PoisonDamageMultiplier; break;
            case DamageTypes.Blood:
                damageMultiplier = _playerStats.BloodDamageMultiplier; break;
        }

        for (int i = 0; i < _attack.Repetitions; i++)
        {
            _bulletLauncher.Launch(new PatternData(_attack.Bullet, curCount, curSpread, curAngleOffset, _attack.RandomAngleOffset, DamageTeam.Player, null, _attack.StartAtFixedAngle ? _attack.FixedAngle : null, true, _previousEffectsInChain), damageMultiplier);

            curCount += _attack.CountModifier;
            curSpread += _attack.SpreadModifier;
            curAngleOffset += _attack.AngleOffsetIncrease;

            yield return new WaitForSeconds(_attack.RepeatDelay / _playerStats.AttackSpeed);
        }

        _attackRoutine = null;
    }
}