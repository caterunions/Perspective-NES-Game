using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManaDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerStats _playerStats;
    [SerializeField]
    private Image _manaBar;
    [SerializeField]
    private TextMeshProUGUI _manaText;

    private void OnEnable()
    {
        _playerStats.OnManaChange += UpdateManaBar;

        UpdateManaBar(_playerStats, 0);
    }

    private void OnDisable()
    {
        _playerStats.OnManaChange -= UpdateManaBar;
    }

    private void UpdateManaBar(PlayerStats playerStats, float manaChange)
    {
        float manaPercentage = _playerStats.CurrentMana / _playerStats.MaxMana;

        _manaText.text = $"{Mathf.Ceil(_playerStats.CurrentMana)}/{Mathf.Ceil(_playerStats.MaxMana)}";

        _manaBar.fillAmount = manaPercentage;
    }
}
