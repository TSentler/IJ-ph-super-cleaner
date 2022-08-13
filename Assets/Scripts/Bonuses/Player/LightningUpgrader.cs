using System;
using System.Collections;
using PlayerAbilities.Move;
using Trash;
using Trash.Boosters;
using UnityEngine;
using UnityEngine.Events;

namespace Bonuses.Player
{
    public class LightningUpgrader : MonoBehaviour
    {
        [SerializeField] private TemporaryBonus _temporaryBonus;
        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private Movement _movement;
        [SerializeField] private SuckerBooster _sucker;

        private void OnValidate()
        {
            if (_temporaryBonus == null)
                Debug.LogWarning("TemporaryBonus was not found!", this);
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
            if (_sucker == null)
                Debug.LogWarning("GarbageSucker was not found!", this);
        }

        private void OnEnable()
        {
            _temporaryBonus.OnTimerStart += TimerStartHandler;
            _temporaryBonus.OnTimerEnd += TimerEndHandler;
            _garbageDisposal.OnSucked += SuckedHandler;
        }
    
        private void OnDisable()
        {
            _temporaryBonus.OnTimerStart -= TimerStartHandler;
            _temporaryBonus.OnTimerEnd -= TimerEndHandler;
            _garbageDisposal.OnSucked -= SuckedHandler;
        }

        private void TimerStartHandler()
        {
            _movement.BoostSpeed();
            _sucker.IncreaseSize();
        }

        private void TimerEndHandler()
        {
            _movement.ResetSpeed();
            _sucker.ResetSize();
        }
        
        private void SuckedHandler(Garbage garbage)
        {
            if (garbage.TryGetComponent<Lightning>(
                    out var lightningBonus))
            {
                _temporaryBonus.Apply();
            }
        }
    }
}