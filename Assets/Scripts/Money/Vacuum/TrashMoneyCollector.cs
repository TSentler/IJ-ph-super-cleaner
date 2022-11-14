using UnityEngine;
using Vacuum;

namespace Money.Vacuum
{
    public class TrashMoneyCollector : MonoBehaviour
    {
        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private MoneyCounter _moneyCounter;
        [Min(0f), SerializeField] private float _factor = 0.25f;
        
        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
        }
        
        private void OnEnable()
        {
            _garbageCounter.TrashPointsChanged += OnTrashPointsChanged;
        }

        private void OnDisable()
        {
            _garbageCounter.TrashPointsChanged -= OnTrashPointsChanged;
        }

        private void OnTrashPointsChanged(float collected)
        {
            var money = collected * _factor;
            _moneyCounter.Collect(money);
        }
    }
}
