using System;
using UnityEngine;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Animator))]
    public class MovementPresenter : MonoBehaviour
    {
        private readonly int _speedHash = Animator.StringToHash("Speed");
    
        private Animator _animator;
        private Vector2 _direction;
    
        [SerializeField] private Movement _movement;
        
        private void OnValidate()
        {
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _movement.OnMove += SetDirection;
        }

        private void OnDisable()
        {
            _movement.OnMove -= SetDirection;
        }
        
        private void SetDirection(Vector2 _direction)
        {
            _animator.SetFloat(_speedHash, _direction.magnitude);
        }
    }
}
