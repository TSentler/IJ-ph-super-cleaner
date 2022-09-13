using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(FixedJoint))]
    public class VacuumThrower : MonoBehaviour
    {
        private FixedJoint _joint, _jointConfig;
        private ThrowObject _throwObject;

        [Min(0), SerializeField] private float _speed = 10f;

        public event UnityAction OnTie, OnBreak;
            
        private void Awake()
        {
            _jointConfig = GetComponent<FixedJoint>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_throwObject != null)
                return;
            
            if (other.TryGetComponent(out ThrowObject throwObject))
            {
                _throwObject = throwObject;
                var rb = _throwObject.Tie(); 
                _joint = gameObject.AddComponent<FixedJoint>();
                _joint.connectedBody = rb;
                //_joint.spring = _jointConfig.spring;
                _joint.breakForce = _jointConfig.breakForce;
                OnTie?.Invoke();
            }
        }
        
        private void OnJointBreak(float breakForce)
        {
            Throw();
        }

        public void Throw()
        {
            if (_throwObject == null)
                return;

            if (_joint != null)
            {
                _joint.connectedBody = null;
                Destroy(_joint);
            }

            var forward = transform.forward;
            forward = new Vector3(forward.x, 0f, forward.z);
            var force = _speed * forward;
            _throwObject.Break(force);
            _throwObject = null;
            OnBreak?.Invoke();
        }
    }
}
