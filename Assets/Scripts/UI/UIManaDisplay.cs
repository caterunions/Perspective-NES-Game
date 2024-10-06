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
    private Image _manaCostBar;
    [SerializeField]
    private TextMeshProUGUI _manaText;
    [SerializeField]
    private SpellInventoryData _spellInventoryData;

    private float _startingManaCostBarPos;

    private void OnEnable()
    {
        _playerStats.OnManaChange += UpdateManaBar;

        UpdateManaBar(_playerStats, 0);

        _startingManaCostBarPos = _manaCostBar.rectTransform.anchoredPosition.x;
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

        if (_spellInventoryData.SelectedSpell != null && _playerStats.CurrentMana >= _spellInventoryData.SelectedSpell.ManaCost)
        {
            float offset = _manaCostBar.rectTransform.rect.width * manaPercentage;
            _manaCostBar.rectTransform.anchoredPosition = new Vector2(_startingManaCostBarPos - _manaCostBar.rectTransform.rect.width + offset, _manaCostBar.rectTransform.anchoredPosition.y);
            _manaCostBar.fillAmount = _spellInventoryData.SelectedSpell.ManaCost / _playerStats.MaxMana;
        }
        else _manaCostBar.fillAmount = 0f;
    }
}
