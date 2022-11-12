using Money;
using TMPro;
using UnityEngine;

namespace UI.Money
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyTotalText : MonoBehaviour
    {
        [SerializeField] private MoneyCounter _moneyCounter;

        private TextMeshProUGUI _text;

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
            SetMoneyText(_moneyCounter.LevelTotal);
            _moneyCounter.Collected += SetMoneyText;
        }

        private void OnDisable()
        {
            _moneyCounter.Collected -= SetMoneyText;
        }

        private void SetMoneyText(int money)
        {
            _text.SetText(_moneyCounter.LevelTotal.ToString());
        }
    }
}
