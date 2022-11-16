using Statistics;
using UnityEngine;

namespace UI.Statistics
{
    public class TrashPointsCollectPresenter : MonoBehaviour
    {
        [SerializeField] private TrashText _allInGamePanel,
            _allInCompletitionPanel;

        private PlayerStatistics _playerStatistics;

        private void OnValidate()
        {
            if (_allInCompletitionPanel == null || _allInGamePanel == null)
                Debug.LogWarning("CountDown was not found!", this);
        }

        private void Awake()
        {
            _playerStatistics = FindObjectOfType<PlayerStatistics>();
        }

        private void OnEnable()
        {
            _playerStatistics.AllTrashPointsChanged += OnAllTrashPointsChanged;
            _playerStatistics.TrashPointsChanged += OnTrashPointsChanged;
        }

        private void OnDisable()
        {
            _playerStatistics.AllTrashPointsChanged -= OnAllTrashPointsChanged;
            _playerStatistics.TrashPointsChanged -= OnTrashPointsChanged;
        }

        private void OnTrashPointsChanged(float collected)
        {
            SetText(_playerStatistics.TrashPoints +
                    _playerStatistics.GetAllTrashPoints());
        }

        private void OnAllTrashPointsChanged()
        {
            SetText(_playerStatistics.GetAllTrashPoints());
        }

        private void SetText(int count)
        {
            _allInGamePanel.SetText(count);
            _allInCompletitionPanel.SetText(count);
        }
    }
}
