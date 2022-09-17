using System;
using System.Collections.Generic;
using System.Linq;
using LevelLoader.UI;
using UnityEngine;

namespace LevelLoader
{
    public class LevelInfo : MonoBehaviour
    {
        [Min(0), SerializeField] private int _number;
        [SerializeField] private List<LevelNumberText> _numberTexts = new ();

        private string Number => _number.ToString();

        private void OnValidate()
        {
            if (_numberTexts.Count == 0)
                Debug.LogWarning("LevelNumberText was not found!", this);
            if (_numberTexts.Count == 1 && _numberTexts[0] == null)
            {
                _numberTexts = Resources.
                    FindObjectsOfTypeAll<LevelNumberText>().ToList();
                Debug.LogWarning("Try find all LevelNumberText!", this);
            }
        }

        private void Awake()
        {
            SetAllLevelNumber();
        }

        private void SetAllLevelNumber()
        {
            foreach (var numberText in _numberTexts)
            {
                numberText.SetNumber(Number);
            }
        }
    }
}
