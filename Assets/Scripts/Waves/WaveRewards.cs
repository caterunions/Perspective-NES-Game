using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveRewards : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner _spawner;

    [SerializeField]
    private WaveRewardDisplay _rewardDisplay;

    [SerializeField]
    private List<TrinketRewards> _trinketRewardList;

    [SerializeField]
    private List<SpellRewards> _spellRewardList;

    private Trinket[] _currentTrinketRewards;
    public Trinket[] CurrentTrinketRewards => _currentTrinketRewards;

    private Spell[] _currentSpellRewards;
    public Spell[] CurrentSpellRewards => _currentSpellRewards;

    public bool RewardsPending { get; private set; } = false;

    private void OnEnable()
    {
        _spawner.OnWaveComplete += GiveRewards;
    }

    private void OnDisable()
    {
        _spawner.OnWaveComplete -= GiveRewards;
    }

    private void GiveRewards(EnemySpawner spawner, int waveNum)
    {
        List<TrinketRewards> trinketRewards = _trinketRewardList.Where(rew => rew.WaveNumber == waveNum).ToList();

        if (trinketRewards.Count == 0)
        {
            RewardsPending = false;
            return;
        }

        RewardsPending = true;
        foreach(TrinketRewards rewards in trinketRewards)
        {
            _currentSpellRewards = new Spell[0];
            _currentTrinketRewards = rewards.GetTrinketOptions();
            _rewardDisplay.gameObject.SetActive(true);
        }
        
    }
}
