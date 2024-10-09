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

    [SerializeField]
    private float _maxOffsetDistance;

    private Vector2 _offset;

    private Vector3 _targetPos
    {
        get
        {
            return (Vector2)_player.transform.position + _offset;
        }
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
            if (_targetPos == null) return float.MaxValue;
            return Vector2.Distance(transform.position, _targetPos);
        }
    }

    private float _moveSpeed
    {
        get
        {
            if (_distToPlayer > 30f) return _stats.MoveSpeed * 5;
            if (_stats.MoveSpeed <= 0) return 0;
            return _stats.MoveSpeed * _moveSpeedMult;
        }
    }

    private float _moveSpeedMult;
    public float MoveSpeedMult => _moveSpeedMult;

    private Vector2 _lastMoveDir;
    public Vector2 LastMoveDir => _lastMoveDir;

    private void OnEnable()
    {
        _moveSpeedMult = Random.Range(0.8f, 1.2f);
        float random = Random.Range(0f, 360f);
        _offset = new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * Random.Range(0f, _maxOffsetDistance);
    }

    private void FixedUpdate()
    {
        if (_player == null) return;
        if (EnemyBrain.Acting) return;

        Vector2? moveDir = null;

        if(_distToPlayer < _minAcceptableDistance)
        {
            moveDir = _targetPos - transform.position;
            moveDir *= -1;
        }
        else if(_distToPlayer > _maxAcceptableDistance)
        {
            moveDir = _targetPos - transform.position;
        }

        if(moveDir == null)
        {
            _rb.velocity = Vector2.zero;
            float random = Random.Range(0f, 360f);
            _offset = new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * Random.Range(0f, _maxOffsetDistance);
        }
        else
        {
            _lastMoveDir = moveDir.Value;
            _rb.velocity = moveDir.Value.normalized * _moveSpeed;
        }
    }
}
