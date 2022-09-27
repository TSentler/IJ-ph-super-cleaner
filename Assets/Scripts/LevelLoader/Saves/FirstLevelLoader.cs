using System;
using System.Collections;
using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class FirstLevelLoader : MonoBehaviour
    {
        private LevelInfo _levelInfo;

        [SerializeField] private GameSaver _saver;

        private void OnValidate()
        {
            if (_saver == null)
                Debug.LogWarning("GameSaver was not found!", this);
        }

        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
        }

        private IEnumerator Start()
        {
            var lastLevel = _saver.GetLevel();
            var levelName = _levelInfo.TutorialName;
            if (lastLevel > -1)
            {
                levelName = _levelInfo.GetName(lastLevel);
            }
            
            SceneManager.LoadScene(levelName);
        }
    }
}
