using System;
using UnityEngine;

namespace Trash
{
    public class GarbageAnimationStop : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Garbage _garbage;
        
        private void OnValidate()
        {
            if (_animator == null)
                Debug.LogWarning("Animator was not found!", this);
        }

        private void OnEnable()
        {
            _garbage.OnSuck += SuckHandler;
        }

        private void OnDisable()
        {
            _garbage.OnSuck -= SuckHandler;
        }

        private void SuckHandler()
        {
            _animator.enabled = false;
        }
    }
}
