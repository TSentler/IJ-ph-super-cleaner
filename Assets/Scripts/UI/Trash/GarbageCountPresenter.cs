using Statistics;
using UnityEngine;

namespace UI.Trash
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private PlayerBag _playerBag;
        
        private void OnValidate()
        {
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void Awake()
        {
            _playerBag = FindObjectOfType<PlayerBag>();
        }

        private void OnEnable()
        {
            OnTargetTrashPointsChanged();
            _playerBag.TrashPointsChanged += OnTrashPointsChanged;
            _playerBag.TargetTrashPointsChanged += OnTargetTrashPointsChanged;
        }

        private void OnDisable()
        {
            _playerBag.TrashPointsChanged -= OnTrashPointsChanged;
            _playerBag.TargetTrashPointsChanged -= OnTargetTrashPointsChanged;
        }
        
        private void OnTargetTrashPointsChanged()
        {
            _collectedText.SetCount(_playerBag.TargetTrashPoints);
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            var collectedRound = _playerBag.TrashPoints;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _playerBag.TargetTrashPoints;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}