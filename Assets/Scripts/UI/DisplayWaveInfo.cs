using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWaveInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _waveText;

    [SerializeField]
    private EnemySpawner _spawner;

    [SerializeField]
    private Image _waveProgressBar;

    private void Update()
    {
        _waveText.text = $"Wave {_spawner.CurrentWave}/{_spawner.TotalWaves}";
        if(_spawner.TotalWaveEnemies > 0) _waveProgressBar.fillAmount = 1 - (float)_spawner.RemainingEnemies / (float)_spawner.TotalWaveEnemies;
    }
}
