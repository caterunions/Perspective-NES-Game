using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform _cursor;
    public Transform Cursor => _cursor;

    [SerializeField]
    private HealthPool _playerHP;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _playerHP.OnHPChange += MonitorGameOver;
    }

    private void OnDisable()
    {
        _playerHP.OnHPChange -= MonitorGameOver;
    }

    private void MonitorGameOver(HealthPool pool, float change)
    {
        if (_playerHP.Health <= 0) SceneManager.LoadScene("MainMenu");
    }
}
