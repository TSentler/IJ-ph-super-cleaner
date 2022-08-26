using System;
using UnityEngine;

public class RobberTarget : MonoBehaviour
{
    private Transform _parent;
    private bool _isPickuped, _isKinematic, _useGravity;

    public void PickUp(Transform carryPosition)
    {
        if (_isPickuped)
            return;
        _isPickuped = true;
        
        if (transform.TryGetComponent(out Rigidbody rb))
        {
            _isKinematic = rb.isKinematic;
            _useGravity = rb.useGravity;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        _parent = transform.parent;
        transform.parent = carryPosition;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        if (_isPickuped == false)
            return;
        _isPickuped = false;
        
        if (transform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = _isKinematic;
            rb.useGravity = _useGravity;
        }
        
        transform.parent = _parent;
    }
}
