using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Vector3 _offset;

    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;

    private void OnValidate()
    {
        if (_target == null)
        {
            throw new MissingReferenceException(
                "Tracking object missing");
        }
    }

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void Update () {
        float interpolation = _speed * Time.deltaTime;

        Vector3 position = transform.position;
        Vector3 targetWithOffset = _target.position +_offset;
        position.x = Mathf.Lerp(position.x,
            targetWithOffset.x, interpolation);
        position.z = Mathf.Lerp(position.z, 
            targetWithOffset.z, interpolation);

        transform.position = position;
    }
}
