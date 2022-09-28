using System;
using UI;
using UnityEngine;

namespace Trash.UI
{
    [RequireComponent(typeof(AllGarbageCollector))]
    public class AllGarbageCountPresenter : MonoBehaviour
    {
        private AllGarbageCollector _allGarbageCollector;

        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private TrashText _allInGamePanel,
            _allInCompletitionPanel;

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
            _allGarbageCollector.OnChange += ChangeHandler;
            _garbageCounter.OnCollect += CollectHandler;
        }

        private void OnDisable()
        {
            _allGarbageCollector.OnChange -= ChangeHandler;
            _garbageCounter.OnCollect -= CollectHandler;
        }

        private void CollectHandler(float collected)
        {
            SetText(_garbageCounter.CollectedAtLevel +
                    _allGarbageCollector.AllTrashRounded);
        }

        private void ChangeHandler()
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
