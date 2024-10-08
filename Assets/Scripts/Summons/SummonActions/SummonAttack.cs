using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SummonAttack : SummonAction
{
    [SerializeField]
    private List<Attack> _attacks = new List<Attack>();

    [SerializeField]
    private BulletLauncher _launcher;

    protected override IEnumerator ActionInstructions()
    {
        foreach (Attack attack in _attacks)
        {
            int curCount = attack.Count;
            float curSpread = attack.Spread;
            float curAngleOffset = attack.AngleOffsetStart;

            float damageMultiplier = 1;

            switch (attack.Bullet.DamageType)
            {
                case DamageTypes.Arcane:
                    damageMultiplier = Summon.Stats.ArcaneDamageMultipler + (Summon.Stats.SummonDamageMultiplier - 1); break;
                case DamageTypes.Fire:
                    damageMultiplier = Summon.Stats.FireDamageMultiplier + (Summon.Stats.SummonDamageMultiplier - 1); break;
                case DamageTypes.Ice:
                    damageMultiplier = Summon.Stats.IceDamageMultiplier + (Summon.Stats.SummonDamageMultiplier - 1); break;
                case DamageTypes.Electric:
                    damageMultiplier = Summon.Stats.ElectricDamageMultiplier + (Summon.Stats.SummonDamageMultiplier - 1); break;
                case DamageTypes.Poison:
                    damageMultiplier = Summon.Stats.PoisonDamageMultiplier + (Summon.Stats.SummonDamageMultiplier - 1); break;
                case DamageTypes.Blood:
                    damageMultiplier = Summon.Stats.BloodDamageMultiplier + (Summon.Stats.SummonDamageMultiplier - 1); break;
            }

            for (int i = 0; i < attack.Repetitions; i++)
            {
                _launcher.Launch(new PatternData(attack.Bullet, curCount, curSpread, curAngleOffset, attack.RandomAngleOffset, DamageTeam.Player, null, attack.StartAtFixedAngle ? attack.FixedAngle : null, false, new List<TrinketEffect>()), damageMultiplier);

                curCount += attack.CountModifier;
                curSpread += attack.SpreadModifier;
                curAngleOffset += attack.AngleOffsetIncrease;

                yield return new WaitForSeconds(attack.RepeatDelay / Summon.Stats.SummonAttackSpeed);
            }

            yield return new WaitForSeconds(attack.EndDelay / Summon.Stats.SummonAttackSpeed);
        }
    }
}
