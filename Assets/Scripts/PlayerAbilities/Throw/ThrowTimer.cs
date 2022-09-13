using System.Collections;
using UnityEngine;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowTimer : MonoBehaviour
    {
        private VacuumThrower _vacuumThrower;
        private Coroutine _timerCoroutine;
        private float _timePassed = float.MaxValue;
        
        [SerializeField] private float _delay = 1f;
        
        private bool IsRun => _timePassed < _delay;
        
        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
        }
        
        private void OnEnable()
        {
            _vacuumThrower.OnTie += TieHandler;
            _vacuumThrower.OnBreak += BreakHandler;
        }

        private void OnDisable()
        {
            _vacuumThrower.OnTie -= TieHandler;
            _vacuumThrower.OnBreak -= BreakHandler;
        }
        
        private IEnumerator TimerCoroutine()
        {
            _timePassed = 0f;
            while (IsRun)
            {
                yield return null;
                _timePassed += Time.deltaTime;
            }

            _timerCoroutine = null;
            _vacuumThrower.Throw();
        }
        
        private void TieHandler()
        {
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }
        
        private void BreakHandler()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }
    }
}