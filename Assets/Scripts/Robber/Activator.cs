using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Robber
{
    public class Activator : MonoBehaviour
    {
        private float _seconds;

        [SerializeField] private RobberAI _robberAI;
        [SerializeField] private GameObject _signalings;
        [Min(0f), SerializeField] private float _minSeconds = 3f, 
            _maxSeconds = 6f;
        
        private void OnValidate()
        {
            if (_robberAI == null)
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
            _robberAI.gameObject.SetActive(true);
            ActivateSignaling();
        }

        private void ActivateSignaling()
        {
            _signalings.SetActive(true);
            _robberAI.OnDeactivate += () => 
                _signalings.SetActive(false);
        }
    }
}
