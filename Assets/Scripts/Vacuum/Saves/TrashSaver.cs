using Saves;
using UnityEngine;

namespace Vacuum.Saves
{
    public class TrashSaver : MonoBehaviour
    {
        private readonly string _trashName = "Trash";
        
        [SerializeField] private AllGarbageCollector _allGarbageCollector;

        private GameSaver _saver;
        
        private void OnValidate()
        {
            if (_allGarbageCollector == null)
                Debug.LogWarning("AllGarbageCollector was not found!", this);
        }
        
        private void Awake()
        {
            _saver = FindObjectOfType<GameSaver>();
            _allGarbageCollector.Initialize(_saver?.Load(_trashName) ?? 0);
        }
        
        private void OnEnable()
        {
            _allGarbageCollector.Changed += Save;
        }

        private void OnDisable()
        {
            _allGarbageCollector.Changed -= Save;
        }

        private void Save()
        {
            _saver?.Save(_trashName, _allGarbageCollector.AllTrashRounded);
        }
    }
}
