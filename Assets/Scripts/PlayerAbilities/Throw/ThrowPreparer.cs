using PlayerAbilities.Move;
using UnityEngine;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowPreparer : MonoBehaviour
    {
        private VacuumThrower _vacuumThrower;
        
        [SerializeField] private Movement _movement;

        private void OnValidate()
        {
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }

        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
        }

        private void OnEnable()
        {
            _vacuumThrower.OnTie += _movement.Stop;
            _vacuumThrower.OnBreak += _movement.Go;
        }

        private void OnDisable()
        {
            _vacuumThrower.OnTie -= _movement.Stop;
            _vacuumThrower.OnBreak -= _movement.Go;

        }
    }
}
