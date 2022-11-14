using UnityEngine;

namespace Vacuum
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalEnvironment : MonoBehaviour
    {
        [SerializeField] private float _speed = 135f;

        private Transform _target;
        private Rigidbody _rigidbody;
        private bool _isTied;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    
        private void FixedUpdate()
        {
            if (_target == null)
                return;

            var deltaSpeed = _speed //* _target.ExtraSpeedMyltiply 
                                    * Time.deltaTime;
            var direction = 
                (_target.position - transform.position).normalized;
            var force = direction * deltaSpeed;
            _rigidbody.AddForce(force, ForceMode.Force);
        }

        public void Suck(Transform target)
        {
            _target = target;
        }
    }
}
