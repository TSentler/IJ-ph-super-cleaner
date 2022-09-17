using System;
using System.Collections.Generic;
using System.Linq;
using LevelLoader.UI;
using UnityEngine;

namespace LevelLoader
{
    [RequireComponent(typeof(LevelInfo))]
    public class LevelName : MonoBehaviour
    {
        private LevelInfo _levelInfo;
        
        [SerializeField] private List<LevelNumberText> _numberTexts = new ();

        private void OnValidate()
        {
            if (_numberTexts.Count == 0)
                Debug.LogWarning("LevelNumberText was not found!", this);
        }

        private void Awake()
        {
            _levelInfo = GetComponent<LevelInfo>();
        }

        private void Start()
        {
            SetAllLevelNumber();
        }

        private void SetAllLevelNumber()
        {
            var number = (_levelInfo.LevelNumber + 1).ToString();
            foreach (var numberText in _numberTexts)
            {
                numberText.SetNumber(number);
            }
        }
    }
}
