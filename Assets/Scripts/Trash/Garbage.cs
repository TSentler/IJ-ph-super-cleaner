using System;
using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    [RequireComponent(typeof(Collider))]
    public abstract class Garbage : MonoBehaviour, ISuckable
    {
        private GarbageDisposal _target;
        
        [Min(0), SerializeField] private float _count = 0f;

        public event UnityAction OnSuck;
        
        public GarbageDisposal Target => _target;
        
        public float Count => _count;

        private bool CheckPositive(float value)
        {
            if (value < 0f)
            {
                Debug.LogWarning("Count of garbage less than 0", this);
            }

            return value > 0f;
        }

        protected abstract void SuckHandler();

        public void SetCount(float value)
        {
            if (_count == 0f && CheckPositive(value))
            {
                _count = value;
            }
        }
        
        public void Suck(GarbageDisposal target)
        {
            _target = target;
            if (Target == null)
                return;
            
            SuckHandler();
            OnSuck?.Invoke();
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
        }
    }
}