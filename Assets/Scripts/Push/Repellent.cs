using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Repellent : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float _speed = 200f;
        
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 point)
    {
        _rb.AddForceAtPosition(Vector3.up * _speed, point, 
            ForceMode.Force);
    }
}
