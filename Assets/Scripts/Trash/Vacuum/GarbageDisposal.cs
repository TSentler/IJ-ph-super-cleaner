using System;
using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    public class GarbageDisposal : MonoBehaviour
    {
        private float _oldSpeed;
        
        [Min(0f), SerializeField] private float _environmentExtraSuckSpeed = 1f,
            _bonusMultiply = 1.2f;
        
        public event UnityAction<Garbage> OnSucked;

        public float ExtraSpeedMyltiply => _environmentExtraSuckSpeed;

        private void Awake()
        {
            _oldSpeed = _environmentExtraSuckSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Sucked();
                OnSucked(garbage);
            }
        }

        public void BoostSpeed()
        {
            _environmentExtraSuckSpeed *= _bonusMultiply;
        }

        public void ResetSpeed()
        {
            _environmentExtraSuckSpeed = _oldSpeed;
        }
    }
}
