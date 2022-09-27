using System;
using System.Collections;
using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class FirstLevelLoader : MonoBehaviour
    {
        private LevelInfo _levelInfo;

        [SerializeField] private GameSaver _saver;
        [SerializeField] private Slider _slider;
        [Min(0f), SerializeField] private float _delay = 10f;

        private void OnValidate()
        {
            if (_saver == null)
                Debug.LogWarning("GameSaver was not found!", this);
            if (_slider == null)
                Debug.LogWarning("Slider was not found!", this);
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

            var ellapsed = 0f;
            while (ellapsed < _delay)
            {
                ellapsed += 0.5f;
                _slider.value = ellapsed / _delay;
                yield return new WaitForSecondsRealtime(0.5f);
            }
            
            SceneManager.LoadScene(levelName);
        }
    }
}
