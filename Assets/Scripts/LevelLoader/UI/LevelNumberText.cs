using System;
using TMPro;
using UnityEngine;

namespace LevelLoader.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelNumberText : MonoBehaviour
    {
        private TMP_Text _text;
        private string _template = $"LEVEL {{0}}";
        private string _number;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            if (_number != string.Empty)
            {
                SetNumber(_number);
            }
        }

        public void SetNumber(string number)
        {
            _number = number;
            _text?.SetText(string.Format(_template, number));
        }
    }
}
