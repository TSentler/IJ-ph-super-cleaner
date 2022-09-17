using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class NextLevelLoader : MonoBehaviour
    {
        private LevelInfo _levelInfo;
        private Coroutine _loadCoroutine;
        private float _delay = 2f;

        [SerializeField] private List<NextLevelButton> 
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
                button.SetNextLevelAction(Apply);
            }
        }

        private void Apply()
        {
            if (_loadCoroutine != null)
                return;

            _loadCoroutine = StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            yield return new WaitForSeconds(_delay);
            SceneManager.LoadScene(_levelInfo.GetNextLevel());
        }

    }
}
