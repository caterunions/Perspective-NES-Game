using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
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
        _animationRoutine = null;
        _animationRoutine = StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        _spriteRenderer.flipX = _aimer.PlayerLeftOfEnemy;

        if(_animationRoutine == null)
        {
            _animationRoutine = StartCoroutine(WalkRoutine());
        }
    }

    private IEnumerator WalkRoutine()
    {
        while(!_mover.WithinRange)
        {
            _spriteRenderer.sprite = _walkSprites[_walkIndex];
            _walkIndex++;
            if(_walkIndex >= _walkSprites.Length) _walkIndex = 0;

            if (_mover.WithinRange) break;

            yield return new WaitForSeconds(_walkSpriteDurations);
        }
        _animationRoutine = null;
    }

    private IEnumerator AttackRoutine()
    {
        _spriteRenderer.sprite = _attackSprite;

        yield return new WaitForSeconds(_attackSpriteDuration);

        _spriteRenderer.sprite = _idleSprite;

        yield return new WaitUntil(() => _mover.WithinRange);

        _animationRoutine = null;
    }
}
