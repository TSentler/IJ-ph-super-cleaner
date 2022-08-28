using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace AI
{
    public class Activator : MonoBehaviour
    {
        private float _seconds;

        [SerializeField] private Robber _robber;
        [SerializeField] private GameObject _signalings;
        [Min(0f), SerializeField] private float _minSeconds = 3f, 
            _maxSeconds = 6f;
        
        private void OnValidate()
        {
            if (_robber == null)
                Debug.LogWarning("Robber gameObject was not found!", this);
            if (_signalings == null)
                Debug.LogWarning("Signalings gameObject was not found!", this);
        }
        
        private void Awake()
        {
            _seconds = Random.Range(_minSeconds, _maxSeconds);
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_seconds);
            _robber.gameObject.SetActive(true);
            ActivateSignaling();
        }

        private void ActivateSignaling()
        {
            _signalings.SetActive(true);
            _robber.OnDeactivate += () => 
                _signalings.SetActive(false);
        }
    }
}
