using LevelCompleter;
using Saves;
using UnityEngine;

namespace Trash.Saves
{
    public class TrashSaver : MonoBehaviour
    {
        private readonly string _trashName = "Trash";
        
        private GameSaver _saver;
        
        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private Completer _completer;

        public int LastTrash { get; private set; }

        private void OnValidate()
        {
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }
        
        private void Awake()
        {
            _saver = FindObjectOfType<GameSaver>();
            LastTrash = _saver?.Load(_trashName) ?? 0;
        }
        
        private void OnEnable()
        {
            _completer.OnComplete += Save;
        }

        private void OnDisable()
        {
            _completer.OnComplete -= Save;
        }

        private void Save()
        {
            var trash = _garbageCounter.Collected + LastTrash;
            LastTrash = trash;
            _saver.Save(_trashName, trash);
        }
    }
}
