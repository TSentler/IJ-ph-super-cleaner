using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Repellent repellent))
        {
            repellent.Push(collision.GetContact(0).point);
        }
    }
}
