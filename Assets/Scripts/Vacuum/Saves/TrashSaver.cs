using Saves;
using UnityEngine;

namespace Vacuum.Saves
{
    public class TrashSaver : MonoBehaviour
    {
        private readonly string _trashName = "Trash";
        
        private GameSaver _saver;
        
        [SerializeField] private AllGarbageCollector _allGarbageCollector;

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
            _allGarbageCollector.OnChange += Save;
        }

        private void OnDisable()
        {
            _allGarbageCollector.OnChange -= Save;
        }

        private void Save()
        {
            _saver?.Save(_trashName, _allGarbageCollector.AllTrashRounded);
        }
    }
}
