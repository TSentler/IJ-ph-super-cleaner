using System;
using System.Collections;
using Trash.Transforms;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Deformator),
        typeof(SizeReducer), 
        typeof(LookAtRotator))]
    public class DeformableGarbage : Garbage
    {
        private Coroutine _suckCoroutine;
        private Deformator _deformator;
        private SizeReducer _sizeReducer;
        private LookAtRotator _lookAtRotator;
        
        private void Awake()
        {
            _deformator = GetComponent<Deformator>();
            _sizeReducer = GetComponent<SizeReducer>();
            _lookAtRotator = GetComponent<LookAtRotator>();
        }

        private void Update()
        {
            if (Target == null)
                return;

            _lookAtRotator.Apply(Target);
        }
        
        private IEnumerator SuckCoroutine()
        {
            var deformationCoroutine = _deformator.Apply();
            yield return deformationCoroutine;
            _sizeReducer.Apply();
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
