using System;
using TMPro;
using UnityEngine;

namespace Money.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyTotalText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        [SerializeField] private MoneyCounter _moneyCounter;

        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
        }

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            SetMoneyText();
            _moneyCounter.OnChange += MoneyChangeHandler;
        }

        private void OnDisable()
        {
            _moneyCounter.OnChange -= MoneyChangeHandler;
        }

        private void MoneyChangeHandler()
        {
            SetMoneyText();
        }

        private void SetMoneyText()
        {
            _text.SetText(_moneyCounter.Total.ToString());
        }
    }
}
