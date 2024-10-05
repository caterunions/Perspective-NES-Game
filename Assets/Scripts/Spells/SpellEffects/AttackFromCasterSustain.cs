using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack From Caster Sustain", menuName = "Scriptable Objects/Spell Effects/Attack From Caster Sustain")]
public class AttackFromCasterSustain : SpellSustainEffect
{
    [SerializeField]
    private Attack _attack;

    private Coroutine _attackRoutine;

    public override void Activate()
    {
        _attackRoutine = CoroutineHelper.Instance.StartCoroutine(AttackRoutine());
    }

    public override void Cancel()
    {
        CoroutineHelper.Instance.StopCoroutine(_attackRoutine);
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
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
                _bulletLauncher.Launch(new PatternData(_attack.Bullet, curCount, curSpread, curAngleOffset, _attack.RandomAngleOffset, DamageTeam.Player, null, _attack.StartAtFixedAngle ? _attack.FixedAngle : null, false, new List<TrinketEffect>()), damageMultiplier);

                curCount += _attack.CountModifier;
                curSpread += _attack.SpreadModifier;
                curAngleOffset += _attack.AngleOffsetIncrease;

                yield return new WaitForSeconds(_attack.RepeatDelay / _playerStats.AttackSpeed);
            }

            yield return new WaitForSeconds(_repeatDelay / _playerStats.AttackSpeed);
        }
    }
}
