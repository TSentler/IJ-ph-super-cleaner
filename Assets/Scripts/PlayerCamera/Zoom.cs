using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(Camera))]
    public class Zoom : MonoBehaviour
    {
        private Camera _camera;

        [Min(0f), SerializeField] private float _minZoom = 110f, 
            _maxZoom = 50f,
            _zoomLimiter = 20f;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void Apply(float greatestDistance)
        {
            var ratio = greatestDistance / _zoomLimiter;
            var targetZoom = Mathf.Lerp(_maxZoom, _minZoom, ratio);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView,
                targetZoom, Time.deltaTime);
        }
    }
}
