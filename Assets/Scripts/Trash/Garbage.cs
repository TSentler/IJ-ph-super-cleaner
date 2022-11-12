using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Trash
{
    [RequireComponent(typeof(Collider))]
    public abstract class Garbage : MonoBehaviour
    {
        [FormerlySerializedAs("_count")] 
        [Min(0), SerializeField] private float _trashPoints = 0f;

        private Transform _target;
        
        public Transform Target => _target;
        public float TrashPoints => _trashPoints;

        public event UnityAction SuckStarted;
        
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
            if (_trashPoints == 0f && CheckPositive(value))
            {
                _trashPoints = value;
            }
        }
        
        public void Suck(Transform target)
        {
            _target = target;
            if (Target == null)
                return;
            
            SuckHandler();
            SuckStarted?.Invoke();
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
        }
    }
}