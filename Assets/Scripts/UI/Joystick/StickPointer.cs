using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StickPointer : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    private readonly float _deadZone = 0.05f;

    private Vector2 _startTouch, _currentTouch, _stickVector;
    private bool _isTouch;
    
    [SerializeField] private RectTransform _stickRect;

    public event UnityAction FingerOut;
    public event UnityAction<Vector2> FingerDown, FingerMove;

    public bool IsTouch => _isTouch;
    public Vector2 Position => _stickVector;
    
    private void OnValidate()
    {
        if (_stickRect == null)
            Debug.LogWarning("RectTransform was not found!", this);
    }
    
    private void Update()
    {
        MovePointer();
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

    private void MovePointer()
    {
        if (_isTouch == false)
            return;

        Vector2 targetTouch;
        if (Input.touchCount == 0)
            targetTouch = Input.mousePosition;
        else
            targetTouch = Input.GetTouch(0).position;

        _stickVector = CalculateStickVector(_startTouch,
            targetTouch);

        FingerMove?.Invoke(_stickVector);
    }
    
    private bool IsPrimaryFinger(int pointerId)
    {
        return pointerId == 0 || pointerId == -1;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.dragging || IsPrimaryFinger(eventData.pointerId) == false)
            return;

        _isTouch = false;
        FingerOut?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPrimaryFinger(eventData.pointerId) == false)
            return;

        _startTouch = eventData.position;
        
        _isTouch = true;
        FingerDown?.Invoke(_startTouch);
    }
}
