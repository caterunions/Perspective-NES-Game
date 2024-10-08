using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _minAcceptableDistance = 5f;

    [SerializeField]
    private float _maxAcceptableDistance = 10f;

    [SerializeField]
    private float _moveSpeed = 1;

    private Summon _summon;
    protected Summon Summon
    {
        get
        {
            if (_summon == null) _summon = GetComponentInParent<Summon>();
            return _summon;
        }
    }

    private float _distToPlayer
    {
        get
        {
            return Vector2.Distance(transform.position, Summon.Spawner.transform.position);
        }
    }

    private void FixedUpdate()
    {
        Vector2? moveDir = null;

        if (_distToPlayer < _minAcceptableDistance)
        {
            moveDir = Summon.Spawner.transform.position - transform.position;
            moveDir *= -1;
        }
        else if (_distToPlayer > _maxAcceptableDistance)
        {
            moveDir = Summon.Spawner.transform.position - transform.position;
        }

        if (moveDir == null)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.velocity = moveDir.Value.normalized * _moveSpeed;
        }
    }
}
