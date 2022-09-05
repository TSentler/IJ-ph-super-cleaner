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
        private Transform _lastTarget;
        
        [SerializeField] private float _speed = 10f;

        private void Awake()
        {
            _deformator = GetComponent<Deformator>();
            _sizeReducer = GetComponent<SizeReducer>();
            _lookAtRotator = GetComponent<LookAtRotator>();
        }

        private IEnumerator LookAtCoroutine()
        {
            while (true)
            {
                _lookAtRotator.Apply(Target);
                yield return null;
            }
        }
        
        private IEnumerator SuckCoroutine()
        {
            StartCoroutine(LookAtCoroutine());
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
            transform.position = Vector3.MoveTowards(
                transform.position, _lastTarget.position, deltaSpeed);
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
