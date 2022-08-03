using System;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private StickPointer _stick;
    [SerializeField] private MovementPresenter _presenter;
    [SerializeField] private Movement _movement;
    
    private void OnValidate()
    {
        if (_stick == null)
            Debug.LogWarning("StickPointer was not found!", this);
        if (_presenter == null)
            Debug.LogWarning("MovementPresenter was not found!", this);
        if (_movement == null)
            Debug.LogWarning("Movement was not found!", this);
    }
    
    private void OnEnable()
    {
        _stick.FingerDown += StickOn;
        _stick.FingerOut += StickOff;
        _stick.FingerMove += Move;
    }

    private void OnDisable()
    {
        _stick.FingerDown -= StickOn;
        _stick.FingerOut -= StickOff;
        _stick.FingerMove -= Move;
    }

    private void Update()
    {
        var inputDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
        if (_stick.IsTouch == false)
        {
            Move(inputDirection);
        }
    }
    
    private void StickOn(Vector2 direction)
    {
        Move(Vector2.zero);
    }
    
    private void StickOff()
    {
        Move(Vector2.zero);
    }
    
    private void Move(Vector2 direction)
    {
        _presenter.SetDirection(direction);
        _movement.Move(direction);
    }
}
