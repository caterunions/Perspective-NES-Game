using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(EntityRadar))]
public class Summon : MonoBehaviour
{
    public event Action<Summon> OnLifetimeEnd;

    private EntityRadar _radar;
    public EntityRadar Radar
    {
        get
        {
            if (_radar == null) _radar = GetComponent<EntityRadar>();
            return _radar;
        }
    }

    private GameObject _spawner;
    public GameObject Spawner => _spawner;

    private PlayerStats _stats;
    public PlayerStats Stats => _stats;

    [SerializeField]
    private float _lifetime;
    public float Lifetime => _lifetime;

    private float _lifetimeEnd;
    public float LifetimeEnd => _lifetimeEnd;

    [SerializeField]
    private List<SummonAction> _actions;

    private SummonAction _curAction;
    private int _actionIndex = 0;

    public bool Acting
    {
        get { return _curAction.InProgress; }
    }

    private bool _canAct
    {
        get { return Radar.CollidersInRadius.Count > 0; }
    }

    public void Initialize(GameObject spawner, PlayerStats stats)
    {
        _spawner = spawner;
        _stats = stats;
        _lifetimeEnd = Time.time + (_lifetime * _stats.SummonLifetimeMultiplier);
    }

    private void OnEnable()
    {
        _actionIndex = 0;
        _curAction = _actions[_actionIndex];
    }

    private void OnDisable()
    {
        _curAction.Stop();
    }

    private void Update()
    {
        if (_lifetimeEnd - Time.time <= 0) OnLifetimeEnd?.Invoke(this);

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
