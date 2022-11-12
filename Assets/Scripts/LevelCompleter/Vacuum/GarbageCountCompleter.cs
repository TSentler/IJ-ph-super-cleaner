using UnityEngine;
using Vacuum;

namespace LevelCompleter.Vacuum
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
        private Completer _completer;

        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private AllGarbageCollector _allGarbageCollector;
        
        private void OnValidate()
        {
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
            if (_allGarbageCollector == null)
                Debug.LogWarning("AllGarbageCollector was not found!", this);
        }
        
        private void Awake()
        {
            _completer = GetComponent<Completer>();
        }
        
        private void OnEnable()
        {
            _garbageCounter.Collected += OnCollected;
            _completer.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            _garbageCounter.Collected -= OnCollected;
            _completer.Completed -= OnCompleted;
        }
        
        private void OnCollected(float collected)
        {
            if (_garbageCounter.Count == _garbageCounter.CollectedAtLevel)
            {
                _completer.Complete();
            }
        }

        private void OnCompleted()
        {
            _garbageCounter.Pause();
            _allGarbageCollector.AddLevelGarbage();
        }
    }
}
