using UnityEngine;
using Vacuum;

namespace Trash.UI
{
    [RequireComponent(typeof(AllGarbageCollector))]
    public class AllGarbageCountPresenter : MonoBehaviour
    {
        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private TrashText _allInGamePanel,
            _allInCompletitionPanel;

        private AllGarbageCollector _allGarbageCollector;

        private void OnValidate()
        {
            if (_allInCompletitionPanel == null || _allInGamePanel == null)
                Debug.LogWarning("CountDown was not found!", this);
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
        }

        private void Awake()
        {
            _allGarbageCollector = GetComponent<AllGarbageCollector>();
        }

        private void OnEnable()
        {
            _allGarbageCollector.Changed += OnChanged;
            _garbageCounter.Collected += OnCollected;
        }

        private void OnDisable()
        {
            _allGarbageCollector.Changed -= OnChanged;
            _garbageCounter.Collected -= OnCollected;
        }

        private void OnCollected(float collected)
        {
            SetText(_garbageCounter.CollectedAtLevel +
                    _allGarbageCollector.AllTrashRounded);
        }

        private void OnChanged()
        {
            SetText(_allGarbageCollector.AllTrashRounded);
        }

        private void SetText(int count)
        {
            _allInGamePanel.SetText(count);
            _allInCompletitionPanel.SetText(count);
        }
    }
}
