using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public event Action<EnemySpawner, int> OnWaveComplete;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private List<EnemyWave> _waves;
    public int TotalWaves => _waves.Count;

    private List<EnemyBrain> _aliveEnemies = new List<EnemyBrain>();

    private int _totalWaveEnemies;
    public int TotalWaveEnemies => _totalWaveEnemies;

    private int _remainingEnemies;
    public int RemainingEnemies => _remainingEnemies;

    private int _waveIndex = 0;
    public int CurrentWave
    {
        get { return _waveIndex; }
    }
    private Coroutine _endRoutine;

    private bool _rewardsPending = false;
    public bool RewardsPending 
    { 
        get { return _rewardsPending; } 
        set
        {
            if (value == false)
            {
                SpawnWave(_waves[_waveIndex]);
                _waveIndex++;
            }
            _rewardsPending = value;
        }
    }

    [SerializeField]
    private DialogueManager _dialogueManager;

    private bool _allowedToStart = false;

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
        if(_allowedToStart && _remainingEnemies == 0 && _waveIndex < _waves.Count)
        {
            if (_waveIndex > 0) OnWaveComplete?.Invoke(this, _waveIndex);

            if (!RewardsPending)
            {
                SpawnWave(_waves[_waveIndex]);
                _waveIndex++;
            }
        }
        if(_waveIndex >= _waves.Count && _endRoutine == null)
        {
            _endRoutine = StartCoroutine(EndRoutine());
        }
    }

    private IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Ending");
    }

    private void OnEnable()
    {
        _dialogueManager.OnDialogueEnd += StartWave;
    }

    private void OnDisable()
    {
        _dialogueManager.OnDialogueEnd -= StartWave;
    }

    private void StartWave(DialogueManager dm)
    {
        _allowedToStart = true;
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