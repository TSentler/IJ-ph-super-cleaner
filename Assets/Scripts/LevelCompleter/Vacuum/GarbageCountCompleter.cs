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
            _garbageCounter.TrashPointsChanged += OnTrashPointsChanged;
            _completer.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            _garbageCounter.TrashPointsChanged -= OnTrashPointsChanged;
            _completer.Completed -= OnCompleted;
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            if (_garbageCounter.TargetTrashPoints == _garbageCounter.TrashPoints)
            {
                _completer.Complete();
            }
        }

        private void OnCompleted()
        {
            _garbageCounter.Stop();
            _allGarbageCollector.AddLevelGarbage();
        }
    }
}
