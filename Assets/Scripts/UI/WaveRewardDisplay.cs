using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRewardDisplay : MonoBehaviour
{
    [SerializeField]
    private WaveRewards _waveRewards;

    [SerializeField]
    private WaveRewardIcon[] _waveRewardIcons = new WaveRewardIcon[8];

    private void OnEnable()
    {
        foreach(WaveRewardIcon icon in _waveRewardIcons)
        {
            icon.OnIconClicked += HandleIconClicked;
        }

        if(_waveRewards.CurrentTrinketRewards.Length > 0)
        {
            SetupTrinketIcons();
        }
        else if(_waveRewards.CurrentSpellRewards.Length > 0)
        {
            SetupSpellIcons();
        }
    }

    private void OnDisable()
    {
        foreach (WaveRewardIcon icon in _waveRewardIcons)
        {
            icon.OnIconClicked -= HandleIconClicked;
        }
    }

    private void HandleIconClicked(WaveRewardIcon icon, Trinket trinket, Spell spell)
    {
        if(trinket != null)
        {
            _waveRewards.SetTrinketSelection(trinket);
        }
        else if(spell != null)
        {
            _waveRewards.SetSpellSelection(spell);
        }
    }

    private void SetupTrinketIcons()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i >= _waveRewards.CurrentTrinketRewards.Length)
            {
                _waveRewardIcons[i].gameObject.SetActive(false);
            }
            else
            {
                _waveRewardIcons[i].gameObject.SetActive(true);
                _waveRewardIcons[i].Spell = null;
                _waveRewardIcons[i].Trinket = _waveRewards.CurrentTrinketRewards[i];
            }
        }
    }

    private void SetupSpellIcons()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i >= _waveRewards.CurrentSpellRewards.Length)
            {
                _waveRewardIcons[i].gameObject.SetActive(false);
            }
            else
            {
                _waveRewardIcons[i].gameObject.SetActive(true);
                _waveRewardIcons[i].Trinket = null;
                _waveRewardIcons[i].Spell = _waveRewards.CurrentSpellRewards[i];
            }
        }
    }
}
