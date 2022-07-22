using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _axisRaw;

    [SerializeField] private float _runSpeed = 150f;
    [SerializeField] private float _rotationSpeed = 15f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void HandleRotation()
    {
        if (_axisRaw.magnitude < 0.05f)
            return;

        var targetRotation = Quaternion.LookRotation(
            new Vector3(_axisRaw.x, 0f, _axisRaw.y));

        transform.rotation = Quaternion.Slerp(
            transform.rotation, targetRotation,
            _rotationSpeed * Time.deltaTime);
    }
    
    private void Update()
    {
        _axisRaw = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        var deltaSpeed = _runSpeed * Time.deltaTime;
        _rb.velocity = new Vector3(
                _axisRaw.x * deltaSpeed,
                0f,
                _axisRaw.y * deltaSpeed);   
        
        HandleRotation();
    }
}
