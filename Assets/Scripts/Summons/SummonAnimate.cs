using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAnimate : MonoBehaviour
{
    [SerializeField]
    private SummonMove _mover;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Summon _summon;

    private float _startTime;

    private void OnEnable()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        _spriteRenderer.flipX = _mover.LastMoveDir.x > 0;

        float a = Mathf.Lerp(1, 0.5f, Mathf.Pow((Time.time - _startTime) / _summon.Lifetime, 2));

        _spriteRenderer.color = new Color(1, 1, 1, a);
    }
}
