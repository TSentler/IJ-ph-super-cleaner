using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalEnvironment : MonoBehaviour, ISuckable
    {
        private Transform _target;
        private Rigidbody _rb;
        
        [SerializeField] private float _speed = 10f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
    
        private void FixedUpdate()
        {
            if (_target == null)
                return;

            var deltaSpeed = _speed * Time.deltaTime;
            var direction = 
                (_target.position - transform.position).normalized;
            var force = direction * deltaSpeed;
            _rb.AddForce(force, ForceMode.Force);
        }

        public void Suck(Transform target)
        {
            _target = target;
        }
    }
}
