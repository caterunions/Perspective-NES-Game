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
            _image.sprite = value.Sprite;
            _trinket = value;
        }
    }

    public Spell Spell { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnIconClicked?.Invoke(this, Trinket, Spell);
    }
}
