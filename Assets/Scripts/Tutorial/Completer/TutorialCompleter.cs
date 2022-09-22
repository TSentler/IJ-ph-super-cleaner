using System.Collections;
using LevelCompleter;
using Trash;
using UnityEngine;

namespace Tutorial
{
    public class TutorialCompleter : MonoBehaviour
    {
        private Coroutine _coroutine;
        
        [SerializeField] private Garbage _robber;
        [SerializeField] private Completer _completer;

        private void OnValidate()
        {
            if (_robber == null)
                Debug.LogWarning("Robber was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }
        
        private void OnEnable()
        {
            _robber.OnSuck += SuckHandler;
        }

        private void OnDisable()
        {
            _robber.OnSuck -= SuckHandler;
        }

        private IEnumerator CompleteCoroutine()
        {
            yield return new WaitForSeconds(1f);
            _completer.Complete();
        }
        
        private void SuckHandler()
        {
            if (_coroutine != null)
                return;
            
            _coroutine = StartCoroutine(CompleteCoroutine());
        }
    }
}
