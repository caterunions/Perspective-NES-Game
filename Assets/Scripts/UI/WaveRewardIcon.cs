using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WaveRewardIcon : MonoBehaviour, IPointerClickHandler
{
    public event Action<WaveRewardIcon, Trinket, Spell> OnIconClicked;

    [SerializeField]
    private Image _image;

    private Trinket _trinket;
    public Trinket Trinket
    {
        get { return _trinket; }
        set
        {
            if(value != null) _image.sprite = value.Sprite;
            _trinket = value;
        }
    }

    private Spell _spell;
    public Spell Spell
    {
        get { return _spell; }
        set
        {
            if(value != null) _image.sprite = value.Icon;
            _spell = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnIconClicked?.Invoke(this, Trinket, Spell);
    }
}
