using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[CreateAssetMenu(fileName = "Attack From Player Every X Attacks", menuName = "Scriptable Objects/Trinket Effect Activations/Attack From Player Every X Attacks")]
public class AttackFromPlayerEveryXAttacks : TrinketEffectActivation
{
    [SerializeField]
    private Attack _attack;

    [SerializeField]
    private int _attacksRequired;

    private int _counter = 0;
    private Coroutine _attackRoutine = null;

    private void Awake()
    {
        _counter = 0;
    }

    public override void Activate()
    {
        _counter++;
        if (_counter >= _attacksRequired)
        {
            _attackRoutine = CoroutineHelper.Instance.StartCoroutine(AttackRoutine());
            _counter = 0;
        }
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
