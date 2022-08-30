using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robber
{
    public class HudActivator : MonoBehaviour
    {
        private FlipToExitBehaviour _flipToExitBehaviour;
        private RunToTargetBehaviour _runToTargetBehaviour;
        private SuckBehaviour _suckBehaviour;

        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _hud;
        
        private void OnValidate()
        {
            if (_animator == null)
                Debug.LogWarning("Animator was not found!", this);
            if (_hud == null)
                Debug.LogWarning("HUD was not found!", this);
        }

        private void Awake()
        {
            _flipToExitBehaviour = 
                _animator.GetBehaviour<FlipToExitBehaviour>();
            _runToTargetBehaviour = 
                _animator.GetBehaviour<RunToTargetBehaviour>();
            _suckBehaviour = 
                _animator.GetBehaviour<SuckBehaviour>();
        }

        private void OnEnable()
        {
            _runToTargetBehaviour.OnRunStart += Activate;
            _flipToExitBehaviour.OnFLipStart += Deactivate;
            _suckBehaviour.OnSuckStart += Deactivate;
        }

        private void OnDisable()
        {
            _runToTargetBehaviour.OnRunStart -= Activate;
            _flipToExitBehaviour.OnFLipStart -= Deactivate;
            _suckBehaviour.OnSuckStart -= Deactivate;
        }

        private void Activate()
        {
            _hud.SetActive(true);
        }

        private void Deactivate()
        {
            _hud.SetActive(false);
        }
    }
}