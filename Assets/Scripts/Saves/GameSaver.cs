using UnityEngine;

namespace Saves
{
    [DisallowMultipleComponent]
    public class GameSaver : MonoBehaviour
    {
        private readonly string _levelName = "Level",
            _moneyName = "Money",
            _trashName = "Trash";

        private int _lastLevel = -1;
        
        public int LastMoney { get; private set; }
        public int LastTrash { get; private set; }

        private void Awake()
        {
            LastMoney = Load(_moneyName);
            LastTrash = Load(_trashName);
        }

        private void Save(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
            PlayerPrefs.Save();
        }

        private int Load(string name)
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

        public void SaveMoney(int number)
        {
            LastMoney = number;
            Save(_moneyName, number);
        }

        public void SaveTrash(int number)
        {
            LastTrash = number;
            Save(_trashName, number);
        }
    }
}
