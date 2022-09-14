using System;
using System.Collections;
using Trash.Transforms;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(Deformator),
        typeof(SizeReducer))]
    public class DeformableGarbage : Garbage
    {
        private Coroutine _suckCoroutine;
        private Deformator _deformator;
        private SizeReducer _sizeReducer;
        private GarbageDisposal _lastTarget;
        
        [SerializeField] private float _speed = 10f;

        private void Awake()
        {
            _deformator = GetComponent<Deformator>();
            _sizeReducer = GetComponent<SizeReducer>();
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

        private void MoveToTarget()
        {
            var deltaSpeed = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, 
                _lastTarget.transform.position, deltaSpeed);
        }
        
        protected override void SuckHandler()
        {
            if (_suckCoroutine == null)
            {
                _lastTarget = Target;
                _suckCoroutine = StartCoroutine(SuckCoroutine());
            }
        }
    }
}
