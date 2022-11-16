using UnityEngine;
using Vacuum;

namespace UI.Trash
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private VacuumBag _vacuumBag;
        
        private void OnValidate()
        {
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void Awake()
        {
            _vacuumBag = FindObjectOfType<VacuumBag>();
        }

        private void OnEnable()
        {
            OnTargetTrashPointsChanged();
            _vacuumBag.TrashPointsChanged += OnTrashPointsChanged;
            _vacuumBag.TargetTrashPointsChanged += OnTargetTrashPointsChanged;
        }

        private void OnDisable()
        {
            _vacuumBag.TrashPointsChanged -= OnTrashPointsChanged;
            _vacuumBag.TargetTrashPointsChanged -= OnTargetTrashPointsChanged;
        }
        
        private void OnTargetTrashPointsChanged()
        {
            _collectedText.SetCount(_vacuumBag.TargetTrashPoints);
        }
        
        private void OnTrashPointsChanged(float collected)
        {
            var collectedRound = _vacuumBag.TrashPoints;
            _collectedText.SetCollected(collectedRound);
            float sliderValue = (float)collectedRound / _vacuumBag.TargetTrashPoints;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}