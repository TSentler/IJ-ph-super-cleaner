using Statistics;
using UnityEngine;
using Vacuum;

namespace LevelCompleter.Vacuum
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
        [SerializeField] private GarbageCounter _garbageCounter;

        private PlayerStatistics _playerStatistics;
        private Completer _completer;

        private void Awake()
        {
            _completer = GetComponent<Completer>();
            _playerStatistics = FindObjectOfType<PlayerStatistics>();
        }
        
        private void OnEnable()
        {
            _playerStatistics.TrashPointsChanged += OnTrashPointsChanged;
            _completer.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            _playerStatistics.TrashPointsChanged -= OnTrashPointsChanged;
            _completer.Completed -= OnCompleted;
        }

        private void OnTrashPointsChanged(float collected)
        {
            if (_garbageCounter.TargetTrashPoints == _playerStatistics.TrashPoints)
            {
                _completer.Complete();
            }
        }

        private void OnCompleted()
        {
            _playerStatistics.AddLevelGarbage(_playerStatistics.TrashPoints);
        }
    }
}
