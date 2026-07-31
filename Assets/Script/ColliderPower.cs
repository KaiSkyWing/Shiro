using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPower : MonoBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
    }

    public void AbleCollider()
    {
        if (_collider != null)
        {
            _collider.enabled = true;
        }
    }

    public void DisableCollider()
    {
        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }
}
