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
    private TrinketInventoryData _trinketInv;

    [SerializeField]
    private SpellInventoryData _spellInv;

    [SerializeField]
    private List<TrinketRewards> _trinketRewardList;

    [SerializeField]
    private List<SpellRewards> _spellRewardList;

    private Trinket[] _currentTrinketRewards;
    public Trinket[] CurrentTrinketRewards => _currentTrinketRewards;

    private Spell[] _currentSpellRewards;
    public Spell[] CurrentSpellRewards => _currentSpellRewards;


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
        TrinketRewards trinketRewards = _trinketRewardList.FirstOrDefault(rew => rew.WaveNumber == waveNum);

        if (trinketRewards != null)
        {
            _currentSpellRewards = new Spell[0];
            _currentTrinketRewards = trinketRewards.GetTrinketOptions();
            _rewardDisplay.gameObject.SetActive(true);

            _spawner.RewardsPending = true;
        }
        else
        {
            _spawner.RewardsPending = false;
        }
    }

    public void SetTrinketSelection(Trinket trinket)
    {
        _spawner.RewardsPending = false;
        _trinketInv.AddTrinket(trinket);
        _rewardDisplay.gameObject.SetActive(false);
    }
}
