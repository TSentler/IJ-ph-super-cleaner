using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDisposal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Garbage garbage))
        {
            garbage.Sucked();
        }
    }
}
