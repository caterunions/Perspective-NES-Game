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
        SpellRewards spellRewards = _spellRewardList.FirstOrDefault(rew => rew.WaveNumber == waveNum);

        if (spellRewards != null)
        {
            _currentSpellRewards = spellRewards.GetSpellOptions();
            _currentTrinketRewards = new Trinket[0];
            _rewardDisplay.gameObject.SetActive(true);

            _spawner.RewardsPending = true;
        }
        else if (trinketRewards != null)
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

    public void SetSpellSelection(Spell spell)
    {
        _spawner.RewardsPending = false;
        int spellSlotToFill = -1;
        for(int i = 0; i < _spellInv.Spells.Length; i++)
        {
            if (_spellInv.Spells[i] == null) spellSlotToFill = i;
        }
        if (spellSlotToFill >= 0) _spellInv.SetCastingSpellSlot(spellSlotToFill, spell);
        else
        {
            _spellInv.SetCastingSpellSlot(_spellInv.SelectedSpellIndex, spell);
        }
        _rewardDisplay.gameObject.SetActive(false);
    }
}
