using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerAbilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowObject : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public Rigidbody Tie()
        {
            _rb.useGravity = false;
            return _rb;
        }
        
        public void Break(Vector3 force)
        {
            _rb.AddForce(force, ForceMode.Impulse);
            _rb.useGravity = true;
        }
    }
}
