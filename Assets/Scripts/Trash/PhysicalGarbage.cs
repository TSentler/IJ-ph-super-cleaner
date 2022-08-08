using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalGarbage : Garbage
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            SetCount(1f);
        }
    
        private void FixedUpdate()
        {
            if (_target == null)
                return;

            var deltaSpeed = _speed * Time.deltaTime;
            var direction = 
                (_target.position - transform.position).normalized;

            _rb.velocity = direction * deltaSpeed;
        }
    }
}
