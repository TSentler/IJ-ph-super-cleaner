using PlayerAbilities.Move;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Movement),
        typeof(Rigidbody), 
        typeof(Animator))]
    public class RunToTargetState : MonoBehaviour
    {
        private RunToTargetBehaviour _runToTargetBehaviour;
        private Movement _movement;
        private Rigidbody _rb;
        private Animator _animator;
        private Vector2 _runDirection;
        
        [SerializeField] private RobberAI _robberAI;
        [Min(0.1f), SerializeField] private float _minDistance = 1f;
        
        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _runToTargetBehaviour = _animator.GetBehaviour<RunToTargetBehaviour>();
            _movement = GetComponent<Movement>();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _runToTargetBehaviour.OnRunStart += RunToTargetStartHandler;
            _runToTargetBehaviour.OnRunUpdate += RunToTargetUpdateHandler;
            _runToTargetBehaviour.OnRunEnd += RunToTargetEndHandler;
        }

        private void OnDisable()
        {
            _runToTargetBehaviour.OnRunStart -= RunToTargetStartHandler;
            _runToTargetBehaviour.OnRunUpdate -= RunToTargetUpdateHandler;
            _runToTargetBehaviour.OnRunEnd -= RunToTargetEndHandler;
        }

        private void RunToTargetStartHandler()
        {
            _rb.isKinematic = false;
            _rb.useGravity = true;
        }
        
        private void RunToTargetUpdateHandler()
        {
            var direction = _robberAI.GetDirectionToTarget();
            _movement.Move(direction.normalized);
            if (direction.magnitude < _minDistance)
            {
                _movement.Move(Vector2.zero);
                _robberAI.PickUpTarget();
            }
        }

        private void RunToTargetEndHandler()
        {
            _movement.Move(Vector2.zero);
        }
    }
}
