using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationStates : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("Speed");
    
    private Animator _animator;
    private Vector2 _axisRaw;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        _axisRaw = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        _animator.SetFloat(_speedHash, _axisRaw.magnitude);
    }
    
}
