using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayWaveInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _waveText;

    [SerializeField]
    private EnemySpawner _spawner;

    private void Update()
    {
        _waveText.text = $"Wave {_spawner.CurrentWave}\nEnemies Remaining: {_spawner.RemainingEnemies}";
    }
}
