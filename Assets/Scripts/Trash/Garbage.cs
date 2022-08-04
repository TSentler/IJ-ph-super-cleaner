using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    [RequireComponent(typeof(Rigidbody))]
    public class Garbage : MonoBehaviour
    {
        private Rigidbody _rb;
        private Transform _target;
    
        [SerializeField] private float _speed = 200.0f;

        public event UnityAction OnSucked;   

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
    
        private void FixedUpdate()
        {
            if (_target == null)
                return;
        
            var deltaSpeed = _speed * Time.deltaTime;
            var direction = (_target.position - transform.position).normalized;

            _rb.velocity = direction * deltaSpeed;
        }
    
        public void Suck(Transform target)
        {
            _target = target;
        }

        public void Sucked()
        {
            gameObject.SetActive(false);
            OnSucked.Invoke();
        }
    }
}
