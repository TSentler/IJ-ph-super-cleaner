using PlayerAbilities.Move;
using UI.Joystick;
using UnityEngine;

namespace PlayerInput
{
    public class MovementInput : MonoBehaviour
    {
        private Vector2 _lastDirection;
        private bool _isPause;
        
        [SerializeField] private StickPointer _stick;
        [SerializeField] private Movement _movement;
        
        private void OnValidate()
        {
            if (_stick == null)
                Debug.LogWarning("StickPointer was not found!", this);
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
            if (_isPause)
            {
                _lastDirection = Vector2.zero;
            }
            else if (_stick.IsTouch == false)
            {
                _lastDirection = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"));
                if (_lastDirection.magnitude > 1f)
                {
                    _lastDirection.Normalize();
                }
            }
            _movement.Move(_lastDirection);
        }
        
        private void StickOn(Vector2 direction)
        {
            _lastDirection = Vector2.zero;
        }
        
        private void StickOff()
        {
            _lastDirection = Vector2.zero;
        }
        
        private void Move(Vector2 direction)
        {
            _lastDirection = direction;
        }

        public void Pause()
        {
            StickOff();
            _isPause = true;
        }
    }
}