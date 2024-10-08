using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// workaround for unity not serializing 2d arrays
[Serializable]
public class EnemyWave
{
    [SerializeField]
    private EnemyWaveSpawns[] _enemyWaveSpawns;
    public EnemyWaveSpawns[] EnemyWaveSpawns => _enemyWaveSpawns;
}
