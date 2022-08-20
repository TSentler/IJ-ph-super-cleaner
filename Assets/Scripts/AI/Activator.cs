using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public class Activator : MonoBehaviour
    {
        private float _seconds;

        [SerializeField] private GameObject _object;
        [Min(0f), SerializeField] private float _minSeconds = 3f, 
            _maxSeconds = 6f;
        
        private void OnValidate()
        {
            if (_object == null)
                Debug.LogWarning("Activatable gameObject was not found!", this);
        }
        
        private void Awake()
        {
            _seconds = Random.Range(_minSeconds, _maxSeconds);
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_seconds);
            _object.SetActive(true);
        }
    }
}
