using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class AllGarbageCollector : MonoBehaviour
    {
        private float _allTrash;
        
        [SerializeField] private GarbageCounter _garbageCounter;

        public event UnityAction Changed;

        public int AllTrashRounded => Mathf.RoundToInt(_allTrash);

        private void OnValidate()
        {
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
        }
        
        public void Initialize(int trash)
        {
            _allTrash = trash;
            Changed?.Invoke();
        }
        
        public void AddLevelGarbage()
        {
            _allTrash += _garbageCounter.CollectedAtLevel;
            Changed?.Invoke();
        }
    }
}
