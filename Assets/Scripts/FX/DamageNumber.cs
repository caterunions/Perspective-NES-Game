using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _text;
    public TextMeshPro Text => _text;

    [SerializeField]
    private float _ascendSpeed;

    [SerializeField]
    private float _lifetime;

    [SerializeField]
    private Color _defaultColor = Color.white;
    public Color DefaultColor => _defaultColor;

    [SerializeField]
    private Color _arcaneColor = Color.white;
    public Color ArcaneColor => _arcaneColor;

    [SerializeField]
    private Color _fireColor = Color.white;
    public Color FireColor => _fireColor;

    [SerializeField]
    private Color _iceColor = Color.white;
    public Color IceColor => _iceColor;

    [SerializeField]
    private Color _electricColor = Color.white;
    public Color ElectricColor => _electricColor;

    [SerializeField]
    private Color _poisonColor = Color.white;
    public Color PoisonColor => _poisonColor;

    [SerializeField]
    private Color _bloodColor = Color.white;
    public Color BloodColor => _bloodColor;

    private void OnEnable()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + _ascendSpeed * Time.deltaTime);
    }
}