using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private float _horizontal;
    private float _vertical;

    [SerializeField] private float _runSpeed = 150.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        var deltaSpeed = _runSpeed * Time.deltaTime;
        _rb.velocity = new Vector3(
                _horizontal * deltaSpeed,
                0f,
                _vertical * deltaSpeed);   
    }
}
