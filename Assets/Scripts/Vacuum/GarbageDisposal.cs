using Statistics;
using Trash;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class GarbageDisposal : MonoBehaviour
    {
        [SerializeField] private PlayerBag _bag;
        [Min(0f), SerializeField] private float _environmentExtraSuckSpeed = 1f,
            _bonusMultiply = 1.2f;
        
        private float _oldSpeed;

        public float ExtraSpeedMyltiply => _environmentExtraSuckSpeed;
        
        public event UnityAction<Garbage> Collected;

        private void Awake()
        {
            _oldSpeed = _environmentExtraSuckSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Sucked();
                _bag.AddTrashPoints(garbage.TrashPoints);
                Collected(garbage);
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
