using Statistics;
using UnityEngine;
using Vacuum;

namespace UI
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private PlayerStatistics _playerStatistics;
        private GarbageCounter _garbageCounter;
        
        private void OnValidate()
        {
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void Awake()
        {
            _playerStatistics = FindObjectOfType<PlayerStatistics>();
            _garbageCounter = FindObjectOfType<GarbageCounter>();
        }

        private void OnEnable()
        {
            _playerStatistics.TrashPointsChanged += OnTrashPointsChanged;
            _garbageCounter.TargetTrashPointsChanged += OnTargetTrashPointsChanged;
        }

        private void OnDisable()
        {
            _playerStatistics.TrashPointsChanged -= OnTrashPointsChanged;
            _garbageCounter.TargetTrashPointsChanged -= OnTargetTrashPointsChanged;
        }
        
        private void OnTargetTrashPointsChanged(int count)
        {
            _collectedText.SetCount(count);
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            var collectedRound = _playerStatistics.TrashPoints;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _garbageCounter.TargetTrashPoints;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}