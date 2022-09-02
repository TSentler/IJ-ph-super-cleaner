using System;
using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    public interface ISuckable
    {
        void Suck(Transform target);
    }

    [RequireComponent(typeof(Collider))]
    public abstract class Garbage : MonoBehaviour, ISuckable
    {
        private Transform _target;
        
        [Min(0), SerializeField] private float _count = 0f;
        [SerializeField] private float _speed = 10f;

        public event UnityAction OnSuck;
        
        public Transform Target => _target;
        public float Count => CheckPositive(_count) ? _count : 0f;

        private bool CheckPositive(float value)
        {
            if (value < 0f)
            {
                Debug.LogWarning("Count of garbage less than 0", this);
            }

            return value > 0f;
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
            if (target == null)
                return;
            _target = target;
            SuckHandler();
            OnSuck?.Invoke();
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
        }
    }
}