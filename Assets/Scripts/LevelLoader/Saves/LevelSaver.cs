using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader.Saves
{
    public class LevelSaver : MonoBehaviour
    {
        private GameSaver _saver;
        
        [SerializeField] private LevelInfo _levelInfo;
        
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
            SceneManager.sceneLoaded += SceneLoadedHandler;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoadedHandler;
        }

        private void SceneLoadedHandler(Scene scene, LoadSceneMode mode)
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
