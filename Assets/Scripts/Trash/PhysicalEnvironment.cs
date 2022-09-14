using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalEnvironment : MonoBehaviour, ISuckable 
    {
        private GarbageDisposal _target;
        private Rigidbody _rb;
        private bool _isTied;
        
        [SerializeField] private float _speed = 10f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
    
        private void FixedUpdate()
        {
            if (_target == null)
                return;

            var deltaSpeed = _speed * _target.ExtraSpeedMyltiply 
                                    * Time.deltaTime;
            var direction = 
                (_target.transform.position - transform.position).normalized;
            var force = direction * deltaSpeed;
            _rb.AddForce(force, ForceMode.Force);
        }

        public void Suck(GarbageDisposal target)
        {
            _target = target;
        }
    }
}
