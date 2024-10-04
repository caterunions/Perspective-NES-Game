using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnDamageNumbersOnHit : MonoBehaviour
{
    private DamageReceiver _dr;
    protected DamageReceiver Dr
    {
        get
        {
            if (_dr == null) _dr = GetComponent<DamageReceiver>();
            return _dr;
        }
    }

    private Collider2D _coll;
    protected Collider2D Coll
    {
        get
        {
            if (_coll == null) _coll = GetComponent<Collider2D>();
            return _coll;
        }
    }

    [SerializeField]
    private DamageNumber _damageNumberObj;

    private void OnEnable()
    {
        Dr.OnDamage += SpawnDamageNumber;
    }

    private void OnDisable()
    {
        Dr.OnDamage -= SpawnDamageNumber;
    }

    private void SpawnDamageNumber(DamageReceiver dr, DamageEvent dmgEvent, DamageResult result)
    {
        Vector3 spawnPos;

        if (Coll != null) spawnPos = new Vector2(transform.position.x, transform.position.y + Coll.bounds.extents.y + 0.5f);
        else spawnPos = dmgEvent.SpecificSource.transform.position;

        DamageNumber dmgNum = Instantiate(_damageNumberObj, spawnPos, Quaternion.identity);

        dmgNum.Text.text = $"<color=#FF0000>-{Mathf.CeilToInt(dmgEvent.Damage)}";
    }
}