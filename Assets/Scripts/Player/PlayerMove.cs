using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private PlayerStats _stats;

    private Vector2 _lastMoveInput;
    public Vector2 LastMoveInput => _lastMoveInput;

    public void HandleMove(Vector2 moveInfo)
    {
        _lastMoveInput = moveInfo;
    }

    private void Update()
    {
        _rb.velocity = _lastMoveInput * _stats.CurrentMoveSpeed;
    }

    private void OnDisable()
    {
        _lastMoveInput = Vector2.zero;
        _rb.velocity = Vector2.zero;
    }
}
