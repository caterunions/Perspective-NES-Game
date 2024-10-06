using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Akaal.PvCustomizer.Editor.Utils.PvIconAttributeCache;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]
    private EntityStats _stats;

    [SerializeField]
    private HealthDamageReceiver _healthDamageReceiver;

    private List<StatusEffect> _activeStatusEffects = new List<StatusEffect>();

    public void AddStatusEffect(StatusEffect statusEffect)
    {
        StatusEffect existingMatch = _activeStatusEffects.FirstOrDefault(eff => eff.StatusEffectData == statusEffect.StatusEffectData);

        if (existingMatch != null)
        {
            if (statusEffect.EndTime > existingMatch.EndTime)
            {
                existingMatch.EndTime = statusEffect.EndTime;
            }

            if(existingMatch.StatusEffectData.Stackable)
            {
                existingMatch.StackCount++;

                foreach (StatBoost statBoost in existingMatch.LastAppliedBoosts)
                {
                    if (_stats.ActiveStatBoosts.Any(sb => sb.Equals(statBoost))) _stats.ActiveStatBoosts.Remove(_stats.ActiveStatBoosts.First(sb => sb.Equals(statBoost)));
                }

                List<StatBoost> lastApplied = new List<StatBoost>();

                foreach (StatBoost statBoost in existingMatch.StatusEffectData.StatBoosts)
                {
                    float newBoost = 0;

                    switch(existingMatch.StatusEffectData.StackType)
                    {
                        case StackTypes.Linear:
                            newBoost = statBoost.StatIncreaseAmount * existingMatch.StackCount;
                            break;
                        case StackTypes.Hyperbolic:
                            if(statBoost.StatIncreaseAmount < 0)
                            {
                                newBoost = 1 - 1 / (1 + (Mathf.Abs(statBoost.StatIncreaseAmount) * existingMatch.StackCount));
                                newBoost *= -1;
                            }
                            else newBoost = 1 - 1/(1 + (statBoost.StatIncreaseAmount * existingMatch.StackCount));
                            break;
                        case StackTypes.Exponential:
                            if (statBoost.StatIncreaseAmount < 0)
                            {
                                newBoost = Mathf.Pow(1 + Mathf.Abs(statBoost.StatIncreaseAmount), existingMatch.StackCount);
                                newBoost *= -1;
                            }
                            else newBoost = Mathf.Pow(1 + statBoost.StatIncreaseAmount, existingMatch.StackCount);
                            break;
                    }

                    StatBoost boost = new StatBoost(statBoost.StatIncrease, statBoost.Type, statBoost.Source, newBoost);

                    lastApplied.Add(boost);
                    _stats.ActiveStatBoosts.Add(boost);
                }
                existingMatch.LastAppliedBoosts = lastApplied;
            }
        }
        else
        {
            _activeStatusEffects.Add(statusEffect);
            List<StatBoost> lastApplied = new List<StatBoost>();
            foreach (StatBoost statBoost in statusEffect.StatusEffectData.StatBoosts)
            {
                lastApplied.Add(statBoost);
                _stats.ActiveStatBoosts.Add(statBoost);
            }
            statusEffect.LastAppliedBoosts = lastApplied;
        }
    }

    private void Update()
    {
        foreach(StatusEffect statusEffect in _activeStatusEffects.Where(eff => eff.EndTime <= Time.time).ToList())
        {
            foreach (StatBoost statBoost in statusEffect.LastAppliedBoosts)
            {
                if (_stats.ActiveStatBoosts.Any(sb => sb.Equals(statBoost))) _stats.ActiveStatBoosts.Remove(_stats.ActiveStatBoosts.First(sb => sb.Equals(statBoost)));
            }
            _activeStatusEffects.Remove(statusEffect);
        }
        foreach (StatusEffect statusEffect in _activeStatusEffects.Where(eff => eff.NextProc <= Time.time))
        {
            statusEffect.NextProc = Time.time + statusEffect.StatusEffectData.TickRate;
            foreach(StatusEffectProc proc in statusEffect.StatusEffectData.Procs)
            {
                proc.ProvideContext(gameObject, _stats, _healthDamageReceiver, statusEffect);
                proc.Proc(statusEffect.StackCount);
            }
        }
    }
}
