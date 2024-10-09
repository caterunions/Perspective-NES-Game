using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveRewardIcon : MonoBehaviour
{
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
}
