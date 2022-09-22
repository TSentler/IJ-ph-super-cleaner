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
            SetMoneyText(_moneyCounter.LevelTotal);
            _moneyCounter.OnCollect += SetMoneyText;
        }

        private void OnDisable()
        {
            _moneyCounter.OnCollect -= SetMoneyText;
        }

        private void SetMoneyText(int money)
        {
            _text.SetText(_moneyCounter.LevelTotal.ToString());
        }
    }
}
