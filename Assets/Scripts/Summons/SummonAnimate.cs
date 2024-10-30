using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAnimate : MonoBehaviour
{
    [SerializeField]
    private SummonMove _mover;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        _spriteRenderer.flipX = _mover.LastMoveDir.x > 0;
    }
}
