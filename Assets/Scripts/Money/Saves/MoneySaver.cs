using Saves;
using LevelCompleter;
using UnityEngine;

namespace Money.Saves
{
    public class MoneySaver : MonoBehaviour
    {
        private readonly string _moneyName = "Money";
            
        [SerializeField] private Store _store;
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private Completer _completer;

        private GameSaver _saver;
        
        private void OnValidate()
        {
            if (_store == null)
                Debug.LogWarning("Store was not found!", this);
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }

        private void Awake()
        {
            _saver = FindObjectOfType<GameSaver>();
            _store.Initialize(_saver?.Load(_moneyName) ?? 0);
        }
        
        private void OnEnable()
        {
            _completer.Completed += EarnLevelMoney;
            _moneyCounter.Collected += EarnMoney;
            _store.Changed += Save;
        }

        private void OnDisable()
        {
            _completer.Completed -= EarnLevelMoney;
            _moneyCounter.Collected -= EarnMoney;
            _store.Changed -= Save;
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

        private void Save()
        {
            _saver.Save(_moneyName, _store.Money);
        }
    }
}
