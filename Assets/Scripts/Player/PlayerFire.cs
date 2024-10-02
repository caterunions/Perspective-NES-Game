using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private BulletLauncher _launcher;

    private bool _firing;
    public bool Firing => _firing;

    private Coroutine _fireRoutine = null;

    private Spell _curSpell;

    public void StartFiring()
    {
        if (_curSpell == null) return;

        _firing = true;
        if(_fireRoutine == null)
        {
            _fireRoutine = StartCoroutine(FireRoutine());
        }
    }

    public void StopFiring()
    {
        _firing = false;
    }

    public void ChangeMainAttackSpell(Spell spell)
    {
        _curSpell = spell;
        StopFiring();
    }

    private IEnumerator FireRoutine()
    {
        while (_firing)
        {
            Attack attack = _curSpell.BasicAttack;

            int curCount = attack.Count;
            float curSpread = attack.Spread;
            float curAngleOffset = attack.AngleOffsetStart;

            for (int i = 0; i < attack.Repetitions; i++)
            {
                _launcher.Launch(new PatternData(attack.Bullet, curCount, curSpread, curAngleOffset, attack.RandomAngleOffset, DamageTeam.Player, null, attack.StartAtFixedAngle ? attack.FixedAngle : null));

                curCount += attack.CountModifier;
                curSpread += attack.SpreadModifier;
                curAngleOffset += attack.AngleOffsetIncrease;

                yield return new WaitForSeconds(attack.RepeatDelay);
            }

            yield return new WaitForSeconds(attack.EndDelay);

            if (!_firing) break;
        }

        _fireRoutine = null;
    }
}