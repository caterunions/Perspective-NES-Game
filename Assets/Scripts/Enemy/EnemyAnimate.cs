using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    private EnemyBrain _brain;
    protected EnemyBrain Brain
    {
        get
        {
            if (_brain == null) _brain = GetComponentInParent<EnemyBrain>();
            return _brain;
        }
    }

    [SerializeField]
    private EnemyAim _aimer;

    [SerializeField]
    private EnemyMove _mover;

    [SerializeField]
    private BulletLauncher _launcher;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite _idleSprite;

    [SerializeField]
    private Sprite[] _walkSprites;

    [SerializeField]
    private float _walkSpriteDurations;

    private int _walkIndex = 0;

    [SerializeField]
    private Sprite _attackSprite;

    [SerializeField]
    private float _attackSpriteDuration;

    private Coroutine _animationRoutine;

    private void OnEnable()
    {
        _launcher.OnPatternLaunch += PerformAttackAnimation;

        _animationRoutine = StartCoroutine(WalkRoutine());
    }

    private void OnDisable()
    {
        _launcher.OnPatternLaunch -= PerformAttackAnimation;
    }

    private void PerformAttackAnimation(BulletLauncher launcher, PatternData pattern, float damageMultiplier)
    {
        _animationRoutine = StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        _spriteRenderer.flipX = _mover.LastMoveDir.x > 0;
        if (Brain.Acting) _spriteRenderer.flipX = _aimer.PlayerLeftOfEnemy;

        if (_animationRoutine == null)
        {
            if(Brain.Acting) _spriteRenderer.sprite = _idleSprite;
            else _animationRoutine = StartCoroutine(WalkRoutine());
        }
    }

    private IEnumerator WalkRoutine()
    {
        while(!_mover.WithinRange)
        {
            _spriteRenderer.sprite = _walkSprites[_walkIndex];
            _walkIndex++;
            if(_walkIndex >= _walkSprites.Length) _walkIndex = 0;

            yield return new WaitForSeconds(_walkSpriteDurations);
        }
        _spriteRenderer.sprite = _idleSprite;
        _animationRoutine = null;
    }

    private IEnumerator AttackRoutine()
    {
        _spriteRenderer.sprite = _attackSprite;

        yield return new WaitForSeconds(_attackSpriteDuration);

        _animationRoutine = null;
    }
}
