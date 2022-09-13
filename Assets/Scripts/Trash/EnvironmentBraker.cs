using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnvironmentBraker : MonoBehaviour
    {
        private Rigidbody _rb;
        private Rigidbody[] _childRigidbodies;
        
        [SerializeField] private GameObject _fragments;
        [SerializeField] private float _maxVelocity = 50f;
        private void OnValidate()
        {
            if (_fragments == null)
                Debug.LogWarning("Fragments was not found!", this);
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _childRigidbodies = GetComponentsInChildren<Rigidbody>(); 
            _fragments.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_rb.velocity.sqrMagnitude > _maxVelocity)
            {
                Break();
            }
        }
        
        private void Break()
        {
            _fragments.transform.parent = transform.parent;
            _fragments.SetActive(true);
            foreach (var rb in _childRigidbodies)
            {
                rb.velocity = _rb.velocity;
            }
            gameObject.SetActive(false);
        }
    }
}
