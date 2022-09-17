using TMPro;
using UnityEngine;

namespace LevelCompleter.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class CountdownTimerText : MonoBehaviour
    {
        private readonly int _oneMinute = 60;
        private readonly string _separator = ":";
        
        private TMP_Text _text;
        
        private string Template => $"{{0}}{_separator}{{1}}";
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetTime(int time)
        {
            int minutes = (int)Mathf.Floor(time / _oneMinute);
            int seconds = time - minutes * _oneMinute;
            _text.SetText(string.Format(Template, 
                minutes.ToString("00"), seconds.ToString("00")));
        }
    }
}
