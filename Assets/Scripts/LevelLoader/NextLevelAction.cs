using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelLoader
{
    public class NextLevelAction : MonoBehaviour
    {
        private UnityAction _nextLevelAction;
        private Coroutine _coroutine; 
        
        [Min(0), SerializeField] private float _delay;

        private IEnumerator ApplyCoroutine()
        {
            yield return new WaitForSeconds(_delay);
            _nextLevelAction?.Invoke();
        }

        public void SetNextLevelAction(UnityAction action)
        {
            _nextLevelAction = action;
        }
        
        public void Apply()
        {
            if (_coroutine != null)
                return;

            _coroutine = StartCoroutine(ApplyCoroutine());
        }
    }
}
