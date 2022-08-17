using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberTarget : MonoBehaviour
{
    public void PickUp(Transform carryPosition)
    {
        if (transform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        transform.parent = carryPosition;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
