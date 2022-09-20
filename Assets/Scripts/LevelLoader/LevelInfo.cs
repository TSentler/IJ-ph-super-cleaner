using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLoader
{
    public class LevelInfo : MonoBehaviour
    {
        private readonly string[] _names = new[]
        {
            "Level1",
            "Level2",
            "Level3",
            "Level4",
            "Level5"
        };
        private readonly string _tutorialName = "Tutorial";
        
        private int _currentLevel = -1;

        public int LevelNumber => _currentLevel;
        public string TutorialName => _tutorialName;
        
        private void Awake()
        {
            var name = SceneManager.GetActiveScene().name;
            for (int i = 0; i < _names.Length; i++)
            {
                if (_names[i] == name)
                {
                    _currentLevel = i;
                }
            }
        }

        public string GetNextLevel()
        {
            var number = _currentLevel + 1;
            if (number == _names.Length)
            {
                number = 0;
            }

            return _names[number];
        }
    }
}
