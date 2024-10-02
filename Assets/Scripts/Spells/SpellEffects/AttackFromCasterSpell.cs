using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack From Caster", menuName = "Scriptable Objects/Spell Effects/Attack From Caster")]
public class AttackFromCasterSpell : SpellEffect
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

        for (int i = 0; i < _attack.Repetitions; i++)
        {
            _bulletLauncher.Launch(new PatternData(_attack.Bullet, curCount, curSpread, curAngleOffset, _attack.RandomAngleOffset, DamageTeam.Player, null, _attack.StartAtFixedAngle ? _attack.FixedAngle : null));

            curCount += _attack.CountModifier;
            curSpread += _attack.SpreadModifier;
            curAngleOffset += _attack.AngleOffsetIncrease;

            yield return new WaitForSeconds(_attack.RepeatDelay);
        }

        _attackRoutine = null;
    }
}
