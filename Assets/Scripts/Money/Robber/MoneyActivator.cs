using System;
using Robber;
using UnityEngine;

namespace Money
{
    [RequireComponent(typeof(Animator))]
    public class MoneyActivator : MonoBehaviour
    {
        private Animator _animator;
        private SuckBehaviour _suckBehaviour;

        [SerializeField] private GameObject _money;
        
        private void OnValidate()
        {
            if (_money == null)
                Debug.LogWarning("Money was not found!", this);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _suckBehaviour = _animator.GetBehaviour<SuckBehaviour>();
        }

        private void OnEnable()
        {
            _suckBehaviour.OnSuckStart += SuckHandler;
        }

        private void OnDisable()
        {
            _suckBehaviour.OnSuckStart -= SuckHandler;
        }
        
        private void SuckHandler()
        {
            _money.transform.parent = null;
            _money.SetActive(true);
        }
    }
}
