using Statistics;
using UnityEngine;

namespace LevelCompleter.Statistics
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
        private PlayerBag _playerBag;
        private Completer _completer;

        private void Awake()
        {
            _completer = GetComponent<Completer>();
            _playerBag = FindObjectOfType<PlayerBag>();
        }
        
        private void OnEnable()
        {
            _playerBag.TrashPointsChanged += OnTrashPointsChanged;
            _completer.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            _playerBag.TrashPointsChanged -= OnTrashPointsChanged;
            _completer.Completed -= OnCompleted;
        }

        private void OnTrashPointsChanged(float collected)
        {
            if (_playerBag.TargetTrashPoints == _playerBag.TrashPoints)
            {
                _completer.Complete();
            }
        }

        private void OnCompleted()
        {
            _playerBag.AddLevelGarbage(_playerBag.TrashPoints);
        }
    }
}
