using UnityEngine;

namespace PlayerAbilities.Push
{
    [RequireComponent(typeof(Rigidbody))]
    public class Repellent : MonoBehaviour
    {
        private Rigidbody _rb;

        [SerializeField] private float _speed = 300f;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Push(Vector3 point)
        {
            _rb.AddForceAtPosition(
                Vector3.up * _speed * Time.deltaTime,
                point, 
                ForceMode.Impulse);
        }
    }
}
