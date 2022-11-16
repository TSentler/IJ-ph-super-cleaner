using UnityEngine;
using Saves;

namespace Statistics
{
    public class AllGarbageCollector
    {
        private float _allTrashPoints;
        private TrashSaver _trashSaver;

        public int AllTrashPointsRounded => Mathf.RoundToInt(_allTrashPoints);

        public AllGarbageCollector()
        {
            _trashSaver = new TrashSaver();
            _allTrashPoints = _trashSaver.Load();
        }
        
        public void AddLevelGarbage(int trashPoints)
        {
            _allTrashPoints += trashPoints;
            _trashSaver.Save(AllTrashPointsRounded);
        }
    }
}
