using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader.Saves
{
    public class LevelSaver : MonoBehaviour
    {
        [SerializeField] private LevelInfo _levelInfo;
        
        private GameSaver _saver;
        
        private void OnValidate()
        {
            if (_levelInfo == null)
                Debug.LogWarning("LevelInfo was not found!", this);
        }
        
        private void Awake()
        {
            _saver = FindObjectOfType<GameSaver>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
#if UNITY_EDITOR
            _saver ??= FindObjectOfType<GameSaver>();
            if(_saver == null)
                return;
#endif
            _saver.SaveLevel(_levelInfo.LevelNumber);
        }
    }
}
