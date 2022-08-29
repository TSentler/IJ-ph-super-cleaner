using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(Camera))]
    public class Zoom : MonoBehaviour
    {
        private Camera _camera;

        [Min(0.0001f),SerializeField] private float _minZoom = 110f,
            _maxZoom = 50f,
            _horizontalLimiter = 7f,
            _verticalLimiter = 20f;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void Apply(Vector2 greatestDistance)
        {
            var ratioX = greatestDistance.x / _horizontalLimiter;
            var ratioY = greatestDistance.y / _verticalLimiter;
            var ratio = Mathf.Max(ratioX, ratioY);

            var targetZoom = Mathf.Lerp(_maxZoom, _minZoom, ratio);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView,
                targetZoom, Time.deltaTime);
        }
    }
}
