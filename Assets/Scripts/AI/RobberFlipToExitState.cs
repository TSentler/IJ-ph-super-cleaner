using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Animator),
        typeof(Rigidbody))]
    public class RobberFlipToExitState : MonoBehaviour
    {
        private FlipToExitBehaviour _flipToExitBehaviour;
        private Animator _animator;
        private Rigidbody _rb;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _flipToExitBehaviour = 
                _animator.GetBehaviour<FlipToExitBehaviour>();
            _rb = GetComponent<Rigidbody>(); 
        }

        private void OnEnable()
        {
            _flipToExitBehaviour.OnFLipStart += FLipToExitStartHandler;
        }

        private void OnDisable()
        {
            _flipToExitBehaviour.OnFLipStart -= FLipToExitStartHandler;
        }

        private void FLipToExitStartHandler()
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
        }
    }
}
