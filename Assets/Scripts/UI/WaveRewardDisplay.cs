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
        if(_waveRewards.CurrentTrinketRewards.Length > 0)
        {
            SetupTrinketIcons();
        }
        else if(_waveRewards.CurrentSpellRewards.Length > 0)
        {
            SetupSpellIcons();
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
                _waveRewardIcons[i].Trinket = _waveRewards.CurrentTrinketRewards[i];
            }
        }
    }

    private void SetupSpellIcons()
    {

    }
}
