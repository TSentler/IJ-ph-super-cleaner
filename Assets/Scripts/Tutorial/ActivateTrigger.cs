using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class ActivateTrigger : MonoBehaviour
    {
        private bool _isActivated;
        
        [SerializeField] private UnityEvent OnTrigger;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isActivated)
                return;
            
            if (other.TryGetComponent(out PlayerTrigger player))
            {
                _isActivated = true;
                OnTrigger?.Invoke();
            }
        }
    }
}
