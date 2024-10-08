using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private BulletLauncher _launcher;

    private float _lastMoveDir = 0;

    private bool _attacking = false;

    private EnemyBrain _enemyBrain;
    protected EnemyBrain EnemyBrain
    {
        get
        {
            if (_enemyBrain == null) _enemyBrain = GetComponentInParent<EnemyBrain>();
            return _enemyBrain;
        }
    }

    private void OnEnable()
    {
        _launcher.OnPatternLaunch += PerformAttackAnimation;
    }

    private void OnDisable()
    {
        _launcher.OnPatternLaunch -= PerformAttackAnimation;
    }

    private void Update()
    {
        if(!_attacking)
        {
            if (_rb.velocity.x > 0)
            {
                _lastMoveDir = _rb.velocity.x;
                // moving right
            }
            else if (_rb.velocity.x < 0)
            {
                _lastMoveDir = _rb.velocity.x;
                // moving left
            }

            _animator.SetFloat("MoveDir", _lastMoveDir);
            _animator.speed = _rb.velocity.magnitude / 10;
        }

        if(!EnemyBrain.Acting)
        {
            _attacking = false;
            _animator.ResetTrigger("AttackRight");
            _animator.ResetTrigger("AttackLeft");
            _animator.SetBool("Attacking", false);
        }
    }

    private void PerformAttackAnimation(BulletLauncher launcher, PatternData pattern, float damageMultiplier)
    {
        _attacking = true;
        _animator.speed = 1f;
        string trigger = "";
        trigger = _lastMoveDir > 0 ? "AttackRight" : "AttackLeft";
        _animator.SetTrigger(trigger);
        _animator.SetBool("Attacking", true);
    }
}
