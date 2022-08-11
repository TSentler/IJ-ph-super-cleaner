using System;
using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    [RequireComponent(typeof(Collider))]
    public abstract class Garbage : MonoBehaviour
    {
        [Min(1), SerializeField] private float _count = 0f;
        
        protected Transform _target;

        [SerializeField] protected float _speed = 10f;

        public event UnityAction<float> OnSucked;

        public float Count => CheckPositive(_count) ? _count : 1f;

        private bool CheckPositive(float value)
        {
            var isPositive = value > 0f;
            if (isPositive == false)
            {
                Debug.LogWarning("Count of garbage less than 1", this);
            }

            return isPositive;
        }
        
        protected virtual void SuckHandler()
        {
            
        }
        
        protected void MoveToTarget()
        {
            var deltaSpeed = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position, _target.position, deltaSpeed);
        }

        public void SetCount(float value)
        {
            if (_count == 0f && CheckPositive(value))
            {
                _count = value;
            }
        }
        
        public void Suck(Transform target)
        {
            _target = target;
            SuckHandler();
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
            OnSucked?.Invoke(Count);
        }
    }
}