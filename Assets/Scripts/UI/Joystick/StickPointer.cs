using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class StickPointer : MonoBehaviour, IDragHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        private readonly float _deadZone = 0.05f;

        private Vector2 _startTouch, _currentTouch, _stickVector;
        private bool _isTouch;
    
        [SerializeField] private RectTransform _stickRect;

        public event UnityAction FingerOut;
        public event UnityAction<Vector2> FingerDown, FingerMove;

        public bool IsTouch => _isTouch;
    
        private void OnValidate()
        {
            if (_stickRect == null)
                Debug.LogWarning("RectTransform was not found!", this);
        }
    
        private Vector2 CalculateStickVector(Vector2 position, Vector2 pressPosition)
        {
            var stickVector = pressPosition - position;
            stickVector /= _stickRect.lossyScale;
            var radius = _stickRect.rect.height / 2;
            stickVector /= radius;
            if (stickVector.magnitude < _deadZone)
                stickVector = Vector2.zero;
            else if (stickVector.magnitude > 1f)
                stickVector.Normalize();

            return stickVector;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isTouch = false;
            FingerOut?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _startTouch = eventData.position;
        
            _isTouch = true;
            FingerDown?.Invoke(_startTouch);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isTouch == false)
                return;

            Vector2 targetTouch = eventData.position;
            _stickVector = CalculateStickVector(_startTouch,
                targetTouch);

            FingerMove?.Invoke(_stickVector);
        }
    }
}
