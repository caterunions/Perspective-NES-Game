using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveRewardDisplay : MonoBehaviour
{
    [SerializeField]
    private WaveRewards _waveRewards;

    [SerializeField]
    private TextMeshProUGUI _description;

    [SerializeField]
    private WaveRewardIcon[] _waveRewardIcons = new WaveRewardIcon[8];

    private void OnEnable()
    {
        _description.text = "";

        foreach (WaveRewardIcon icon in _waveRewardIcons)
        {
            icon.OnIconClicked += HandleIconClicked;
            icon.OnIconEntered += HandleIconEntered;
            icon.OnIconExit += HandleIconExit;
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
            icon.OnIconEntered -= HandleIconEntered;
            icon.OnIconExit -= HandleIconExit;
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

    private void HandleIconEntered(WaveRewardIcon icon, Trinket trinket, Spell spell)
    {
        if (trinket != null)
        {
            _description.text = trinket.Description;
        }
        else if (spell != null)
        {
            _description.text = spell.Description;
        }
    }

    private void HandleIconExit(WaveRewardIcon icon, Trinket trinket, Spell spell)
    {
        _description.text = "";
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
