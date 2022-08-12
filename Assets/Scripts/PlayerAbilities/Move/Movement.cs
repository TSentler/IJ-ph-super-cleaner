using UnityEngine;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;
        private Vector2 _moveDirection;
        private float _currentSpeed;

        [SerializeField] private float _runSpeed = 150f,
            _speedMultiply = 1.5f,
            _rotationSpeed = 15f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _currentSpeed = _runSpeed;
        }

        private void HandleRotation()
        {
            if (_moveDirection.magnitude < 0.05f)
                return;

            var targetRotation = Quaternion.LookRotation(
                new Vector3(_moveDirection.x, 0f, _moveDirection.y));

            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation,
                _rotationSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaSpeed = _currentSpeed * Time.deltaTime;
            _rb.velocity = new Vector3(
                    _moveDirection.x * deltaSpeed,
                    0f,
                    _moveDirection.y * deltaSpeed);   
            
            HandleRotation();
        }
        
        public void Move(Vector2 direction)
        {
            _moveDirection = direction;
        }

        public void BoostSpeed()
        {
            _currentSpeed = _runSpeed * _speedMultiply;
        }

        public void ResetSpeed()
        {
            _currentSpeed = _runSpeed;
        }
    }
}
