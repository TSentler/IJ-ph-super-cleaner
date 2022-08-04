using UnityEngine;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Animator))]
    public class MovementPresenter : MonoBehaviour
    {
        private readonly int _speedHash = Animator.StringToHash("Speed");
    
        private Animator _animator;
        private Vector2 _direction;
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    
        public void SetDirection(Vector2 _direction)
        {
            _animator.SetFloat(_speedHash, _direction.magnitude);
        }
    }
}
