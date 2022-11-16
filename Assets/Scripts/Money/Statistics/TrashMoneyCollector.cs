using Statistics;
using UnityEngine;

namespace Money.Statistics
{
    public class TrashMoneyCollector : MonoBehaviour
    {
        [SerializeField] private MoneyCounter _moneyCounter;
        [Min(0f), SerializeField] private float _factor = 0.25f;
        
        private PlayerStatistics _playerStatistics;
        
        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
        }

        private void Awake()
        {
            _playerStatistics = FindObjectOfType<PlayerStatistics>();
        }

        private void OnEnable()
        {
            _playerStatistics.TrashPointsChanged += OnTrashPointsChanged;
        }

        private void OnDisable()
        {
            _playerStatistics.TrashPointsChanged -= OnTrashPointsChanged;
        }

        private void OnTrashPointsChanged(float collected)
        {
            var money = collected * _factor;
            _moneyCounter.Collect(money);
        }
    }
}
