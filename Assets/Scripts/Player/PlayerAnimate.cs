using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private BulletLauncher _bulletLauncher;

    [SerializeField]
    private PlayerAim _playerAim;

    [SerializeField]
    private PlayerMove _playerMove;

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
        if (_playerAim.transform.rotation.eulerAngles.z > 180) _spriteRenderer.flipX = true;
        else _spriteRenderer.flipX = false;

        if (_playerMove.LastMoveInput != Vector2.zero) _anim.SetBool("Walking", true);
        else _anim.SetBool("Walking", false);
    }

    private void AttackAnim(BulletLauncher launcher, PatternData pattern, float damageMult, bool fromBullet)
    {
        if(!fromBullet) _anim.SetTrigger("Attack");
    }
}
