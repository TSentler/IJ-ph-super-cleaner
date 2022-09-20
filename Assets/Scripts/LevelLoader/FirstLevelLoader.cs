using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class FirstLevelLoader : MonoBehaviour
    {
        private LevelInfo _levelInfo;

        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
        }

        private void Start()
        {
            var levelName = _levelInfo.TutorialName;
            
            SceneManager.LoadScene(levelName);
        }
    }
}
