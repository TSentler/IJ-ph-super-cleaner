using System;
using LevelCompleter.UI;
using UnityEngine;

namespace LevelCompleter
{
    [RequireComponent(typeof(CountdownTimerCompleter))]
    public class CountdownTimerPresenter : MonoBehaviour
    {
        private CountdownTimerCompleter _timer;

        [SerializeField] private CountdownTimerText _timerText;
        
        private void OnValidate()
        {
            if (_timerText == null)
                Debug.LogWarning("CountdownTimerText was not found!", this);
        }

        private void Awake()
        {
            _timer = GetComponent<CountdownTimerCompleter>();
        }

        private void OnEnable()
        {
            _timer.OnChange += TimerChangeHandler;
        }

        private void OnDisable()
        {
            _timer.OnChange -= TimerChangeHandler;
        }

        private void TimerChangeHandler(int time)
        {
            _timerText.SetTime(time);
        }
    }
}
