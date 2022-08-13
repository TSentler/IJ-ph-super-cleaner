using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Bonuses
{
    public class TemporaryBonus : MonoBehaviour
    {
        private float _timePassed = float.MaxValue;

        [SerializeField] private float _duration = 1f;

        public event UnityAction OnTimerStart, OnTimerEnd;
        public event UnityAction<float> OnTimerChange;

        private bool IsRun => _timePassed < _duration;
        
        private IEnumerator BonusCoroutine()
        {
            OnTimerStart?.Invoke();
            _timePassed = 0f;
            while (IsRun)
            {
                yield return null;
                _timePassed += Time.deltaTime;
                var ratio = _timePassed / _duration;
                OnTimerChange?.Invoke(ratio);
            }
            OnTimerEnd?.Invoke();
        }
        
        public void Apply()
        {
            if (IsRun)
            {
                _timePassed = 0f;
            }
            else
            {
                StartCoroutine(BonusCoroutine());
            }
        } 
    }
}
