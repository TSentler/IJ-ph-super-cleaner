using Statistics;
using UnityEngine;

namespace LevelCompleter.Statistics
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
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
            if (_playerStatistics.TargetTrashPoints == _playerStatistics.TrashPoints)
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
