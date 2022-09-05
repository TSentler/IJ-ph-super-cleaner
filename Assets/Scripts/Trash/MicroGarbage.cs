using System.Collections;
using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        private readonly float _minRotationRatio = 0.01f,
            _maxRotationRatio = 1, _minVelocity = 0.01f;
            
        private Coroutine _suckCoroutine;
        private Vector3 _direction;
        private float _startDistance, _velocity;
        
        [SerializeField] private float _axeleration = 1f,
            _damp = 5f;

        private IEnumerator SuckCoroutine()
        {
            while (Target != null || _velocity > _minVelocity)
            {
                if (Target != null)
                {
                    _velocity += _axeleration * Time.deltaTime;
                }
                else
                {
                    _velocity -= _damp * Time.deltaTime;
                }

                MoveToTarget();
                yield return null;
            }

            _suckCoroutine = null;
        }

        private void MoveToTarget()
        {
            if (Target != null)
            {
                _direction = CalculateDirection(Target.position, _direction);
            }
            transform.position += _direction * _velocity;
        }

        private Vector3 CalculateDirection(Vector3 targetPosition, 
            Vector3 currentDirection)
        {
            var direction = targetPosition - transform.position;
            var distance = direction.magnitude;
            direction.Normalize();
            var distanceRatio = distance / _startDistance;
            var rotationRatio = Mathf.Lerp(_maxRotationRatio,
                _minRotationRatio, distanceRatio);
            return Vector3.Lerp(currentDirection, direction, rotationRatio);
        }

        protected override void SuckHandler()
        {
            if (_suckCoroutine != null)
                return;
            
            _direction = Target.position - transform.position;
            _startDistance = _direction.magnitude;
            _direction.Normalize();
            _velocity = 0f;
            _suckCoroutine = StartCoroutine(SuckCoroutine());
        }
    }
}