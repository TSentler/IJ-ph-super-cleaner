using LevelCompleter;
using UnityEngine;

namespace Money.LevelCompleter
{
    public class MoneyPerLevel : MonoBehaviour
    {
        [SerializeField] private Store _store;
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private Completer _completer;

        private void OnValidate()
        {
            if (_store == null)
                Debug.LogWarning("Store was not found!", this);
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }

        private void OnEnable()
        {
            _completer.Completed += EarnLevelMoney;
            _moneyCounter.Collected += EarnMoney;
        }

        private void OnDisable()
        {
            _completer.Completed -= EarnLevelMoney;
            _moneyCounter.Collected -= EarnMoney;
        }
        
        private void EarnLevelMoney()
        {
            EarnMoney(_moneyCounter.LevelTotal);
        }

        private void EarnMoney(int money)
        {
            if (_completer.IsCompleted == false)
                return;
            
            _store.Earn(money);
        }
    }
}
