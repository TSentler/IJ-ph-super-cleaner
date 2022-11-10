using UI;
using UnityEngine;
using Vacuum;

namespace Trash
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private void OnValidate()
        {
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void OnEnable()
        {
            _garbageCounter.OnCollect += CollectHandler;
            _garbageCounter.OnCountChange += CountChangeHandler;
        }

        private void OnDisable()
        {
            _garbageCounter.OnCollect -= CollectHandler;
            _garbageCounter.OnCountChange -= CountChangeHandler;
        }
        
        private void CountChangeHandler(int count)
        {
            _collectedText.SetCount(count);
        }
        
        private void CollectHandler(float collected)
        {
            var collectedRound = _garbageCounter.CollectedAtLevel;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _garbageCounter.Count;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}