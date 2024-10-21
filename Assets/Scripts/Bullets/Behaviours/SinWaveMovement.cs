using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveMovement : BulletBehaviour
{
    [SerializeField]
    private float _frequency;

    [SerializeField]
    private float _magnitude;

    private float _timeAlive;

    [SerializeField]
    private bool _useStartDirection = false;

    private Vector2 _dir;

    private void OnEnable()
    {
        if(_useStartDirection) _dir = transform.right;
    }

    private void Update()
    {
        if (!_useStartDirection) _dir = transform.right;

        _timeAlive += Time.deltaTime;
        transform.position = 
            new Vector2(transform.position.x, transform.position.y) + 
            _dir * 
            Mathf.Cos(_timeAlive * _frequency) * (_magnitude * Time.deltaTime);
    }
}
