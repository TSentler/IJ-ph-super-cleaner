using System;
using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    public abstract class Garbage : MonoBehaviour
    {
        protected Transform _target;
        
        [Min(1), SerializeField] private float _count = 0f;

        [SerializeField] protected float _speed = 200.0f;

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
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
            OnSucked?.Invoke(Count);
        }
    }
}