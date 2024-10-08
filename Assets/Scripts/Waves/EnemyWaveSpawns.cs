using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWaveSpawns
{
    [SerializeField]
    private Transform _spawnPoint;
    public Transform SpawnPoint => _spawnPoint;

    [SerializeField]
    private EnemyBrain _enemy;
    public EnemyBrain Enemy => _enemy;

    [SerializeField]
    private int _count = 1;
    public int Count => _count;

    [SerializeField]
    private float _spawnInterval;
    public float SpawnInterval => _spawnInterval;

    [SerializeField]
    private float _startDelay;
    public float StartDelay => _startDelay;

    private EnemySpawner _spawner;

    private Coroutine _spawnRoutine;

    public void SpawnWave(EnemySpawner spawner)
    {
        _spawner = spawner;
        _spawnRoutine = CoroutineHelper.Instance.StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(StartDelay);

        for (int i = 0; i < _count; i++)
        {
            _spawner.SpawnEnemy(Enemy, _spawnPoint.position);

            yield return new WaitForSeconds(SpawnInterval);
        }

        _spawnRoutine = null;
    }
}
