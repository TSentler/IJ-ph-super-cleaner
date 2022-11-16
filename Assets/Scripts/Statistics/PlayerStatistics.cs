using UnityEngine;
using UnityEngine.Events;

namespace Statistics
{
    public class PlayerStatistics : MonoBehaviour
    {
        private float _trashPoints = 0f;
        private float _targetTrashPoints = 0f;
        private AllGarbageCollector _allGarbageCollector;
        private bool _isStopCollectTrashPoints;
        
        public int TrashPoints => Mathf.RoundToInt(_trashPoints);
        public int TargetTrashPoints => Mathf.RoundToInt(_targetTrashPoints);


        public event UnityAction<float> TrashPointsChanged;
        public event UnityAction AllTrashPointsChanged;
        public event UnityAction TargetTrashPointsChanged;

        private void Awake()
        {
            _allGarbageCollector = new AllGarbageCollector();
            AllTrashPointsChanged?.Invoke();
        }
        
        public int GetAllTrashPoints()
        {
            return _allGarbageCollector.AllTrashPointsRounded;
        }

        public void AddLevelGarbage(int garbageCounterTrashPoints)
        {
            _isStopCollectTrashPoints = true;
            _allGarbageCollector.AddLevelGarbage(garbageCounterTrashPoints);
            AllTrashPointsChanged?.Invoke();
        }

        public void AddTrashPoints(float trashPoints)
        {
            if (_isStopCollectTrashPoints)
                return;
            
            _trashPoints += trashPoints;
            TrashPointsChanged?.Invoke(trashPoints);
        }

        public void AddTarrgetTrashPoints(float targetTrashPoints)
        {
            _targetTrashPoints += targetTrashPoints;
            TargetTrashPointsChanged?.Invoke();
        }
    }
}
