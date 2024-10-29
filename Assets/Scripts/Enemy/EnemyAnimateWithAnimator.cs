using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// god im so sorry this class even exists
public class EnemyAnimateWithAnimator : MonoBehaviour
{
    [SerializeField]
    private EnemyMove _mover;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private BulletLauncher _bulletLauncher;

    [SerializeField]
    private EnemyBrain _brain;

    [SerializeField]
    private Animator _anim;

    private void OnEnable()
    {
        _bulletLauncher.OnPatternLaunch += AttackAnim;
    }

    private void OnDisable()
    {
        _bulletLauncher.OnPatternLaunch -= AttackAnim;
    }

    private void Update()
    {
        _spriteRenderer.flipX = _mover.LastMoveDir.x > 0;

        _anim.SetBool("Walking", !_brain.Acting);
    }

    private void AttackAnim(BulletLauncher launcher, PatternData pattern, float damageMult, bool fromBullet)
    {
        if (!fromBullet) _anim.SetTrigger("Attack");
    }
}
