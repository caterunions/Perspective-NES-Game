using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EntityRadar : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerToCheck;

    private List<Collider2D> _collidersInRadius;
    public List<Collider2D> CollidersInRadius => _collidersInRadius;
    public List<Transform> TransformsInRadius
    {
        get { return CollidersInRadius.Select(c => c.transform).ToList();  }
    }

    [SerializeField]
    private float _radius;

    private ContactFilter2D _contactFilter;

    private void OnEnable()
    {
        _collidersInRadius = new List<Collider2D>();
        _contactFilter = new ContactFilter2D();
        _contactFilter.layerMask = _layerToCheck;
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        Physics2D.OverlapCircle(transform.position, _radius, _contactFilter, _collidersInRadius);
    }
}
