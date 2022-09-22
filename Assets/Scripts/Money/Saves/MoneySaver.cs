using System;
using Saves;
using LevelCompleter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Money.Saves
{
    public class MoneySaver : MonoBehaviour
    {
        private GameSaver _saver;
        
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private Completer _completer;

        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }

        private void Awake()
        {
            _saver = FindObjectOfType<GameSaver>();
        }
        
        private void OnEnable()
        {
            _completer.OnComplete += SaveMoney;
            _moneyCounter.OnCollect += SaveMoney;
        }

        private void OnDisable()
        {
            _completer.OnComplete -= SaveMoney;
            _moneyCounter.OnCollect -= SaveMoney;
        }
        
        private void SaveMoney(int money)
        {
            if (_completer.IsCompleted == false)
                return;
            
            money += _saver.LastMoney;
            _saver.SaveMoney(money);
        }

        private void SaveMoney()
        {
            SaveMoney(_moneyCounter.LevelTotal);
        }
    }
}
