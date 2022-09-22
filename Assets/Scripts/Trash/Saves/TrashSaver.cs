using LevelCompleter;
using Saves;
using UnityEngine;

namespace Trash.Saves
{
    public class TrashSaver : MonoBehaviour
    {
        private GameSaver _saver;
        
        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private Completer _completer;

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
            var trash = _garbageCounter.Collected + _saver.LastTrash;
            _saver.SaveTrash(trash);            
        }
    }
}
