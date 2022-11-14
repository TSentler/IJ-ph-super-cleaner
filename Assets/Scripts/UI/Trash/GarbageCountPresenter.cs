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
            _garbageCounter.TrashPointsChanged += OnTrashPointsChanged;
            _garbageCounter.TargetTrashPointsChanged += OnTargetTrashPointsChanged;
        }

        private void OnDisable()
        {
            _garbageCounter.TrashPointsChanged -= OnTrashPointsChanged;
            _garbageCounter.TargetTrashPointsChanged -= OnTargetTrashPointsChanged;
        }
        
        private void OnTargetTrashPointsChanged(int count)
        {
            _collectedText.SetCount(count);
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            var collectedRound = _garbageCounter.TrashPoints;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _garbageCounter.TargetTrashPoints;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}