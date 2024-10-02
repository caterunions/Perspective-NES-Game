using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Image _cooldown;

    [SerializeField]
    private CastingHandler _castingHandler;

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    private void Update()
    {
        _cooldown.fillAmount = _castingHandler.CurSpellCooldown / _castingHandler.MaxSpellCooldown;
    }
}
