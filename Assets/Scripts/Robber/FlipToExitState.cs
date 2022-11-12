using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator),
        typeof(Rigidbody))]
    public class FlipToExitState : MonoBehaviour
    {
        private FlipToExitBehaviour _flipToExitBehaviour;
        private Animator _animator;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _flipToExitBehaviour = 
                _animator.GetBehaviour<FlipToExitBehaviour>();
            _rigidbody = GetComponent<Rigidbody>(); 
        }

        private void OnEnable()
        {
            _flipToExitBehaviour.Started += OnFlipToExitStarted;
        }

        private void OnDisable()
        {
            _flipToExitBehaviour.Started -= OnFlipToExitStarted;
        }

        private void OnFlipToExitStarted()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }
    }
}
