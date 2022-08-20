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
        
        [SerializeField] private Robber _robber;
        
        private void OnValidate()
        {
            if (_robber == null)
                Debug.LogWarning("Robber was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _flipToExitBehaviour = _animator.GetBehaviour<FlipToExitBehaviour>();
            _rb = GetComponent<Rigidbody>(); 
        }

        private void OnEnable()
        {
            _flipToExitBehaviour.OnRunStart += RunToExitStartHandler;
            _flipToExitBehaviour.OnRunEnd += RunToExitEndHandler;
        }

        private void OnDisable()
        {
            _flipToExitBehaviour.OnRunStart -= RunToExitStartHandler;
            _flipToExitBehaviour.OnRunEnd -= RunToExitEndHandler;
        }

        private void RunToExitStartHandler()
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
        }

        private void RunToExitEndHandler()
        {
            gameObject.SetActive(false);
        }
    }
}
