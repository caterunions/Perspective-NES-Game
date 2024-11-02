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

    [SerializeField]
    private GameObject _gameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _playerHP.OnHPChange += MonitorGameOver;
        _gameOver.SetActive(false);
    }

    private void OnDisable()
    {
        _playerHP.OnHPChange -= MonitorGameOver;
    }

    private void MonitorGameOver(HealthPool pool, float change)
    {
        if (_playerHP.Health <= 0)
        {
            _playerHP.transform.root.gameObject.SetActive(false);
            _gameOver.SetActive(true);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
