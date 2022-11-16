using Statistics;
using UnityEngine;

namespace UI.Trash
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private PlayerStatistics _playerStatistics;
        
        private void OnValidate()
        {
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void Awake()
        {
            _playerStatistics = FindObjectOfType<PlayerStatistics>();
        }

        private void OnEnable()
        {
            OnTargetTrashPointsChanged();
            _playerStatistics.TrashPointsChanged += OnTrashPointsChanged;
            _playerStatistics.TargetTrashPointsChanged += OnTargetTrashPointsChanged;
        }

        private void OnDisable()
        {
            _playerStatistics.TrashPointsChanged -= OnTrashPointsChanged;
            _playerStatistics.TargetTrashPointsChanged -= OnTargetTrashPointsChanged;
        }
        
        private void OnTargetTrashPointsChanged()
        {
            _collectedText.SetCount(_playerStatistics.TargetTrashPoints);
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            var collectedRound = _playerStatistics.TrashPoints;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _playerStatistics.TargetTrashPoints;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}