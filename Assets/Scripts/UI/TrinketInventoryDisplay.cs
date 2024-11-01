using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrinketInventoryDisplay : MonoBehaviour
{
    [SerializeField]
    private TrinketInventoryData _trinketInv;

    [SerializeField]
    private Image _trinketIcon;

    private List<Image> _trinketIcons = new List<Image>();

    private void OnEnable()
    {
        _trinketInv.OnTrinketAdd += HandleTrinketAdd;
    }

    private void OnDisable()
    {
        _trinketInv.OnTrinketAdd -= HandleTrinketAdd;
    }

    private void HandleTrinketAdd(TrinketInventoryData trinketInv, Trinket trinket)
    {
        Image trinketIcon = Instantiate(_trinketIcon, transform);
        trinketIcon.sprite = trinket.Sprite;
        _trinketIcons.Add(trinketIcon);
    }
}
