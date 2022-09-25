using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class NextLevelLoader : MonoBehaviour
    {
        private LevelInfo _levelInfo;

        [SerializeField] private NextLevelButton _nextLevelButtons;
        
        private void OnValidate()
        {
            if (_nextLevelButtons == null)
                Debug.LogWarning("NextLevelButton was not found!", this);
        }
        
        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
            _nextLevelButtons.SetNextLevelAction(Load);
        }

        private void Load()
        {
            SceneManager.LoadScene(_levelInfo.GetNextLevel());
        }
    }
}
