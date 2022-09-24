using UnityEngine;

namespace Saves
{
    [DisallowMultipleComponent]
    public class GameSaver : MonoBehaviour
    {
        private readonly string _levelName = "Level";

        private int _lastLevel = -1;
        
        public void Save(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
            PlayerPrefs.Save();
        }

        public int Load(string name)
        {
            if (PlayerPrefs.HasKey(name) == false)
            {
                return 0;
            }
            return PlayerPrefs.GetInt(name);
        }

        public void SaveLevel(int number)
        {
            if (number < 0 || _lastLevel == number)
                return;

            _lastLevel = number;
            Save(_levelName, number);
        }

        public int GetLevel()
        {
            _lastLevel = PlayerPrefs.HasKey(_levelName)
                ? Load(_levelName)
                : -1;
            return _lastLevel;
        }
    }
}
