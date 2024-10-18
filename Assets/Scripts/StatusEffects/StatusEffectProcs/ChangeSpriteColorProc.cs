using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Change Sprite Color", menuName = "Scriptable Objects/Status Effect Procs/Change Sprite Color")]
public class ChangeSpriteColorProc : StatusEffectProc
{
    [SerializeField]
    private Color _color = Color.white;

    [SerializeField]
    private float _duration;

    public override void Proc(int stacks)
    {
        SpriteRenderer spr = _entity.GetComponentInChildren<SpriteRenderer>();

        if (spr == null) return;

        CoroutineHelper.Instance.StartCoroutine(ColorRoutine(spr));
    }

    private IEnumerator ColorRoutine(SpriteRenderer spr)
    {
        if (spr != null) spr.color = _color;

        yield return new WaitForSeconds(_duration);

        if(spr != null) spr.color = Color.white;
    }
}
