using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField]
    protected float _maxHealth;
    public float MaxHealth => _maxHealth;

    [SerializeField]
    protected float _regen;
    public float Regen => _regen;
}
