using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LookAtIK : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private bool ikActive = false;
    [SerializeField] private Transform _lookObj = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {

    }
}
