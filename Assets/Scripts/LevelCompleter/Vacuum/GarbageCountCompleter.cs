using UnityEngine;
using Vacuum;

namespace LevelCompleter.Statistics
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
        private VacuumBag _vacuumBag;
        private Completer _completer;

        private void Awake()
        {
            _completer = GetComponent<Completer>();
            _vacuumBag = FindObjectOfType<VacuumBag>();
        }
        
        private void OnEnable()
        {
            _vacuumBag.TrashPointsChanged += OnTrashPointsChanged;
            _completer.Completed += OnCompleted;
        }

        private void OnDisable()
        {
            _vacuumBag.TrashPointsChanged -= OnTrashPointsChanged;
            _completer.Completed -= OnCompleted;
        }

        private void OnTrashPointsChanged(float collected)
        {
            if (_vacuumBag.TargetTrashPoints == _vacuumBag.TrashPoints)
            {
                _completer.Complete();
            }
        }

        private void OnCompleted()
        {
            _vacuumBag.AddLevelGarbage(_vacuumBag.TrashPoints);
        }
    }
}
