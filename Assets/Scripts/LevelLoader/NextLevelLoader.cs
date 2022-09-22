using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class NextLevelLoader : MonoBehaviour
    {
        private LevelInfo _levelInfo;

        [SerializeField] private List<NextLevelAction> 
            _nextLevelButtons = new();
        
        private void OnValidate()
        {
            if (_nextLevelButtons.Count == 0)
                Debug.LogWarning("NextLevelButton was not found!", this);
        }
        
        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
            foreach (var button in _nextLevelButtons)
            {
                button.SetNextLevelAction(Load);
            }
        }

        private void Load()
        {
            SceneManager.LoadScene(_levelInfo.GetNextLevel());
        }
    }
}
