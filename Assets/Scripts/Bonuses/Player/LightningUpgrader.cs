using System.Collections;
using PlayerAbilities.Move;
using Trash;
using Trash.Boosters;
using UnityEngine;

namespace Bonuses.Player
{
    public class LightningUpgrader : MonoBehaviour
    {
        private float _timePassed = float.MaxValue;
        private Coroutine _coroutine;

        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private Movement _movement;
        [SerializeField] private SuckerBooster _sucker;
        [SerializeField] private float _duration = 1f;

        private void OnValidate()
        {
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
            if (_sucker == null)
                Debug.LogWarning("GarbageSucker was not found!", this);
        }
    
        private void OnEnable()
        {
            _garbageDisposal.OnSucked += SuckedHandler;
        }
    
        private void OnDisable()
        {
            _garbageDisposal.OnSucked -= SuckedHandler;
        }

        private void SuckedHandler(Garbage garbage)
        {
            if (garbage.TryGetComponent<Lightning>(
                    out var lightningBonus))
            {
                if (_coroutine != null && _timePassed < _duration)
                {
                    _timePassed = 0f;
                }
                else
                {
                    _coroutine = StartCoroutine(LightningCoroutine());
                }
            }
        }
        
        public IEnumerator LightningCoroutine()
        {
            _movement.BoostSpeed();
            _sucker.IncreaseSize();
            _timePassed = 0f;
            while (_timePassed < _duration)
            {
                yield return null;
                _timePassed += Time.deltaTime;
            }
            _movement.ResetSpeed();
            _sucker.ResetSize();
            _coroutine = null;
        }
    }
}