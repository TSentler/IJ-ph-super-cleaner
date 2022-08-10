using System;
using System.Collections;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Deformator), typeof(SizeReducer))]
    public class DeformableGarbage : Garbage
    {
        private Coroutine _suckCoroutine;
        private Deformator _deformator;
        private SizeReducer _sizeReducer;
        
        private void Awake()
        {
            _deformator = GetComponent<Deformator>();
            _sizeReducer = GetComponent<SizeReducer>();
            SetCount(1f);
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            transform.LookAt(_target);
        }

        private IEnumerator SuckCoroutine()
        {
            var deformationCoroutine = _deformator.Apply();
            yield return deformationCoroutine;
            _sizeReducer.Apply();
            StartCoroutine(MoveCoroutine());
        }
        
        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                MoveToTarget();
                yield return null;
            }
        }

        protected override void SuckHandler()
        {
            if (_suckCoroutine == null)
            {
                _suckCoroutine = StartCoroutine(SuckCoroutine());
            }
        }
    }
}
