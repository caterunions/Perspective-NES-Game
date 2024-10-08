using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private List<EnemyWave> _waves;

    private List<EnemyBrain> _aliveEnemies = new List<EnemyBrain>();

    private int _totalWaveEnemies;
    public int TotalWaveEnemies => _totalWaveEnemies;

    private int _remainingEnemies;
    public int RemainingEnemies => _remainingEnemies;

    private int _waveIndex = 0;

    public void SpawnEnemy(EnemyBrain enemy, Vector2 position)
    {
        EnemyBrain e = Instantiate(enemy, position, Quaternion.identity);
        e.SetPlayer(_player);
        _aliveEnemies.Add(e);

        e.DamageReceiver.OnDamage += MonitorEnemyHealth;
    }

    public void SpawnWave(EnemyWave wave)
    {
        _totalWaveEnemies = wave.EnemyWaveSpawns.Select(spawns => spawns.Count).Sum();
        _remainingEnemies = _totalWaveEnemies;

        foreach(EnemyWaveSpawns spawn in wave.EnemyWaveSpawns)
        {
            spawn.SpawnWave(this);
        }
    }

    private void Update()
    {
        if(_remainingEnemies == 0 && _waveIndex < _waves.Count)
        {
            SpawnWave(_waves[_waveIndex]);
            _waveIndex++;
        }
    }

    private void DestroyEnemy(EnemyBrain enemy)
    {
        if (!_aliveEnemies.Contains(enemy)) return;

        _aliveEnemies.Remove(enemy);
        _remainingEnemies--;
        enemy.DamageReceiver.OnDamage -= MonitorEnemyHealth;

        Destroy(enemy.gameObject);
    }

    private void MonitorEnemyHealth(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        if (!result.Killed) return;

        EnemyBrain enemy = dr.GetComponentInParent<EnemyBrain>();

        DestroyEnemy(enemy);
    }
}