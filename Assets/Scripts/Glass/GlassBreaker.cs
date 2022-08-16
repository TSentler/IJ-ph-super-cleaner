using System;
using UnityEngine;
using AI;

namespace Glass
{
    public class GlassBreaker : MonoBehaviour
    {
        [SerializeField] private GameObject _fragments;

        private void OnValidate()
        {
            if (_fragments == null)
                Debug.LogWarning("Fragments was not found!", this);
        }

        private void Awake()
        {
            _fragments.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Robber robber))
            {
                _fragments.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
