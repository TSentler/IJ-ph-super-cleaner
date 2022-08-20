using PlayerAbilities.Move;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Movement),
        typeof(Rigidbody), 
        typeof(Animator))]
    public class RobberRunToTargetState : MonoBehaviour
    {
        private readonly int _carryName = Animator.StringToHash("Carry");
        
        private RunToTargetBehaviour _runToTargetBehaviour;
        private Movement _movement;
        private Rigidbody _rb;
        private Animator _animator;
        private Vector2 _runDirection;
        
        [SerializeField] private Robber _robber;
        [Min(0.1f), SerializeField] private float _minDistance = 1f;
        
        private void OnValidate()
        {
            if (_robber == null)
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
            Debug.Log("fuck");
            _rb.isKinematic = false;
            _rb.useGravity = true;
        }
        
        private void RunToTargetUpdateHandler()
        {
            var direction = _robber.GetDirectionToTarget();
            _movement.Move(direction.normalized);
            if (direction.magnitude < _minDistance)
            {
                _movement.Move(Vector2.zero);
                _animator.SetTrigger(_carryName);
                _robber.PickUpTarget();
            }
        }

        private void RunToTargetEndHandler()
        {
            _movement.Move(Vector2.zero);
        }
    }
}
