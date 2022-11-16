using Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class VacuumBag : MonoBehaviour
    {
        private float _trashPoints = 0f;
        private float _targetTrashPoints = 0f;
        private bool _isStopCollectTrashPoints;
        private float _allTrashPoints;
        private TrashSaver _trashSaver;
        
        public int TrashPoints => Mathf.RoundToInt(_trashPoints);
        public int TargetTrashPoints => Mathf.RoundToInt(_targetTrashPoints);
        public int AllTrashPointsRounded => Mathf.RoundToInt(_allTrashPoints);

        public event UnityAction<float> TrashPointsChanged;
        public event UnityAction AllTrashPointsChanged;
        public event UnityAction TargetTrashPointsChanged;

        private void Awake()
        {
            _trashSaver = new TrashSaver();
            _allTrashPoints = _trashSaver.Load();
            AllTrashPointsChanged?.Invoke();
        }
        
        public void AddLevelGarbage(int trashPoints)
        {
            _isStopCollectTrashPoints = true;
            _allTrashPoints += trashPoints;
            _trashSaver.Save(AllTrashPointsRounded);
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
