using Statistics;
using UnityEngine;

namespace UI.Statistics
{
    public class TrashPointsCollectPresenter : MonoBehaviour
    {
        [SerializeField] private TrashText _allInGamePanel,
            _allInCompletitionPanel;

        private PlayerBag _playerBag;

        private void OnValidate()
        {
            if (_allInCompletitionPanel == null || _allInGamePanel == null)
                Debug.LogWarning("CountDown was not found!", this);
        }

        private void Awake()
        {
            _playerBag = FindObjectOfType<PlayerBag>();
        }

        private void OnEnable()
        {
            _playerBag.AllTrashPointsChanged += OnAllTrashPointsChanged;
            _playerBag.TrashPointsChanged += OnTrashPointsChanged;
        }

        private void OnDisable()
        {
            _playerBag.AllTrashPointsChanged -= OnAllTrashPointsChanged;
            _playerBag.TrashPointsChanged -= OnTrashPointsChanged;
        }

        private void OnTrashPointsChanged(float collected)
        {
            SetText(_playerBag.TrashPoints +
                    _playerBag.AllTrashPointsRounded);
        }

        private void OnAllTrashPointsChanged()
        {
            SetText(_playerBag.AllTrashPointsRounded);
        }

        private void SetText(int count)
        {
            _allInGamePanel.SetText(count);
            _allInCompletitionPanel.SetText(count);
        }
    }
}
