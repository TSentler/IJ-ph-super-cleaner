using PlayerAbilities.Move;
using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Movement),
        typeof(Rigidbody), 
        typeof(Animator))]
    public class RunToExitState : MonoBehaviour
    {
        private readonly int _flipToExitName = Animator.StringToHash("FlipToExit");
        
        private RunToExitBehaviour _runToExitBehaviour;
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
            _runToExitBehaviour = _animator.GetBehaviour<RunToExitBehaviour>();
            _movement = GetComponent<Movement>();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _runToExitBehaviour.OnRunEnd += RunToExitEndHandler;
            _runToExitBehaviour.OnRunUpdate += RunToExitUpdateHandler;
        }

        private void OnDisable()
        {
            _runToExitBehaviour.OnRunEnd -= RunToExitEndHandler;
            _runToExitBehaviour.OnRunUpdate -= RunToExitUpdateHandler;
        }

        private void RunToExitEndHandler()
        {
            _movement.Move(Vector2.zero);
        }
        
        private void RunToExitUpdateHandler()
        {
            var direction = _robberAI.GetDirectionToExit();
            _movement.Move(direction.normalized);
            if (direction.magnitude < _minDistance)
            {
                _movement.Move(Vector2.zero);
                transform.rotation = Quaternion.LookRotation(
                    new Vector3(direction.x, 0f, direction.y));
                 
                _animator.SetTrigger(_flipToExitName);
                _robberAI.PickUpTarget();
            }
        }
    }
}
