using UnityEngine;

namespace PlayerCamera
{
    public class Follow : MonoBehaviour
    {
        private Vector3 _velocity;
        
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothTime = 0.3f;

        public void Apply(Vector3 centerPoint)
        {
            Vector3 targetPosition = centerPoint + _offset;
            transform.position = Vector3.SmoothDamp(transform.position,
                targetPosition, ref _velocity, _smoothTime);
        }
    }
}
