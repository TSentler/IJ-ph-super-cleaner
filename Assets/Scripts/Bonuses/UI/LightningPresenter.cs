using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bonuses.UI
{
    public class LightningPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _sliderRoot;
        [SerializeField] private Slider _slider;
        [SerializeField] private TemporaryBonus _temporaryBonus;
        
        private void OnValidate()
        {
            if (_sliderRoot == null)
                Debug.LogWarning("SliderRoot was not found!", this);
            if (_slider == null)
                Debug.LogWarning("Slider was not found!", this);
            if (_temporaryBonus == null)
                Debug.LogWarning("TemporaryBonus was not found!", this);
        }

        private void Awake()
        {
            _sliderRoot.SetActive(false);
        }

        private void OnEnable()
        {
            _temporaryBonus.OnTimerStart += TimerStartHandler;
            _temporaryBonus.OnTimerChange += TimerChangeHandler;
            _temporaryBonus.OnTimerEnd += TimerEndHandler;
        }
    
        private void OnDisable()
        {
            _temporaryBonus.OnTimerStart -= TimerStartHandler;
            _temporaryBonus.OnTimerChange -= TimerChangeHandler;
            _temporaryBonus.OnTimerEnd -= TimerEndHandler;
        }

        private void TimerStartHandler()
        {
            _slider.value = _slider.maxValue;
            _sliderRoot.SetActive(true);
        }
        
        private void TimerChangeHandler(float ratio)
        {
            _slider.value = 1f - ratio;
        }

        private void TimerEndHandler()
        {
            _sliderRoot.SetActive(false);
        }
    }
}
