using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private HealthPool _healthPool;
    protected HealthPool healthPool
    {
        get
        {
            if (_healthPool == null) _healthPool = GetComponent<HealthPool>();
            return _healthPool;
        }
    }

    [SerializeField]
    private List<EnemyAction> _actions;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private EnemyAim _aimer;

    [SerializeField]
    private EnemyMove _mover;

    private EnemyAction _curAction;
    private int _actionIndex = 0;

    public bool Acting
    {
        get { return _curAction.InProgress; }
    }

    private bool _canAct
    {
        get { return _mover.WithinRange; }
    }

    public void LockAimer()
    {
        _aimer.Locked = true;
    }

    public void UnlockAimer()
    {
        _aimer.Locked = false;
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;

        _aimer.Player = _player.transform;
        _mover.Player = _player.transform;
    }

    private void OnEnable()
    {
        // remove when spawner logic is added
        _aimer.Player = _player.transform;
        _mover.Player = _player.transform;

        _actionIndex = 0;
        _curAction = _actions[_actionIndex];
    }

    private void OnDisable()
    {
        _curAction.Stop();
    }

    private void Update()
    {
        if (_curAction.InProgress == false && _canAct)
        {
            if (_curAction != _actions[_actionIndex] && !_actions[_actionIndex].InProgress)
            {
                _curAction = _actions[_actionIndex];
            }

            _curAction.Act();

            if (_actionIndex < _actions.Count - 1) _actionIndex++;
            else _actionIndex = 0;
        }
    }
}