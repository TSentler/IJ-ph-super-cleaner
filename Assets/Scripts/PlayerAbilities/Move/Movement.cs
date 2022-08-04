using UnityEngine;

namespace PlayerAbilities.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;
        private Vector2 _moveDirection;

        [SerializeField] private float _runSpeed = 150f;
        [SerializeField] private float _rotationSpeed = 15f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
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
            var deltaSpeed = _runSpeed * Time.deltaTime;
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
    }
}
