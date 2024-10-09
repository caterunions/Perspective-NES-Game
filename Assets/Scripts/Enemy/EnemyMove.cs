using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float _minAcceptableDistance = 5f;

    [SerializeField] 
    private float _maxAcceptableDistance = 10f;

    [SerializeField]
    private EntityStats _stats;

    [SerializeField]
    private Rigidbody2D _rb;

    private Transform _player;
    public Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private EnemyBrain _enemyBrain;
    protected EnemyBrain EnemyBrain
    {
        get
        {
            if (_enemyBrain == null) _enemyBrain = GetComponentInParent<EnemyBrain>();
            return _enemyBrain;
        }
    }

    public bool WithinRange
    {
        get
        {
            if (_player == null) return false;
            if (_distToPlayer > _maxAcceptableDistance || _distToPlayer < _minAcceptableDistance) return false;
            return true;
        }
    }

    private float _distToPlayer
    {
        get
        {
            if (_player == null) return float.MaxValue;
            return Vector2.Distance(transform.position, _player.position);
        }
    }

    private float _moveSpeed
    {
        get
        {
            if (_distToPlayer > 25f) return _stats.MoveSpeed * 5;
            if (_stats.MoveSpeed <= 0) return 0;
            return _stats.MoveSpeed;
        }
    }

    private Vector2 _lastMoveDir;
    public Vector2 LastMoveDir => _lastMoveDir;

    private void FixedUpdate()
    {
        if (_player == null) return;
        if (EnemyBrain.Acting) return;

        Vector2? moveDir = null;

        if(_distToPlayer < _minAcceptableDistance)
        {
            moveDir = _player.position - transform.position;
            moveDir *= -1;
        }
        else if(_distToPlayer > _maxAcceptableDistance)
        {
            moveDir = _player.position - transform.position;
        }

        if(moveDir == null)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _lastMoveDir = moveDir.Value;
            _rb.velocity = moveDir.Value.normalized * _moveSpeed;
        }
    }
}
