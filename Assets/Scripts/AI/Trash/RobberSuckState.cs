using System;
using Trash;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Animator),
        typeof(Rigidbody),
        typeof(Garbage))]
    public class RobberSuckState : MonoBehaviour
    {
        private readonly int _suckName = Animator.StringToHash("Suck");
        
        private Animator _animator;
        private SuckBehaviour _suckBehaviour;
        private Rigidbody _rb;
        private Garbage _garbage;

        [SerializeField] private Robber _robber;
        
        private void OnValidate()
        {
            if (_robber == null)
                Debug.LogWarning("Robber was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _suckBehaviour = _animator.GetBehaviour<SuckBehaviour>();
            _rb = GetComponent<Rigidbody>();
            _garbage = GetComponent<Garbage>();
        }

        private void OnEnable()
        {
            _garbage.OnSuck += SuckHandler;
            _suckBehaviour.OnSuckStart += SuckAnimationStartHandler;
        }

        private void OnDisable()
        {
            _garbage.OnSuck -= SuckHandler;
            _suckBehaviour.OnSuckStart -= SuckAnimationStartHandler;
        }

        private void SuckHandler()
        {
            _robber.DropTarget();
            _animator.SetTrigger(_suckName);
        }

        private void SuckAnimationStartHandler()
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
        }
    }
}
