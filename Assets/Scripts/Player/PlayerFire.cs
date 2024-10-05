using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private PlayerStats _stats;
    private PlayerStats stats
    {
        get
        {
            if (_stats == null) _stats = GetComponentInParent<PlayerStats>();
            return _stats;
        }
    }

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

    public void ChangeSpell(Spell spell)
    {
        _curSpell = spell;
        if(_firing)
        {
            StopFiring();
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    private IEnumerator FireRoutine()
    {
        while (_firing)
        {
            Attack attack = _curSpell.BasicAttack;

            float damageMultiplier = 1;

            switch(attack.Bullet.DamageType)
            {
                case DamageTypes.Arcane:
                    damageMultiplier = stats.ArcaneDamageMultipler; break;
                case DamageTypes.Fire:
                    damageMultiplier = stats.FireDamageMultiplier; break;
                case DamageTypes.Ice:
                    damageMultiplier = stats.IceDamageMultiplier; break;
                case DamageTypes.Electric:
                    damageMultiplier = stats.ElectricDamageMultiplier; break;
                case DamageTypes.Poison:
                    damageMultiplier = stats.PoisonDamageMultiplier; break;
                case DamageTypes.Blood:
                    damageMultiplier = stats.BloodDamageMultiplier; break;
            }

            int curCount = attack.Count;
            float curSpread = attack.Spread;
            float curAngleOffset = attack.AngleOffsetStart;

            for (int i = 0; i < attack.Repetitions; i++)
            {
                _launcher.Launch(new PatternData(attack.Bullet, curCount, curSpread, curAngleOffset, attack.RandomAngleOffset, DamageTeam.Player, null, attack.StartAtFixedAngle ? attack.FixedAngle : null, false, new List<TrinketEffect>()), damageMultiplier);

                curCount += attack.CountModifier;
                curSpread += attack.SpreadModifier;
                curAngleOffset += attack.AngleOffsetIncrease;

                yield return new WaitForSeconds(attack.RepeatDelay / stats.AttackSpeed);
            }

            yield return new WaitForSeconds(attack.EndDelay / stats.AttackSpeed);

            if (!_firing) break;
        }

        _fireRoutine = null;
    }
}