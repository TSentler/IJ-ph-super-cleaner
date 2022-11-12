using Money;
using TMPro;
using UnityEngine;

namespace UI.Money
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StoreMoneyTotalText : MonoBehaviour
    {
        [SerializeField] private Store _store;

        private TextMeshProUGUI _text;
        private CountDown _countDown;

        private void OnValidate()
        {
            if (_store == null)
                Debug.LogWarning("Store was not found!", this);
        }

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            TryGetComponent(out _countDown);
        }

        private void OnEnable()
        {
            SetMoneyText();
            _store.Changed += SetMoneyText;
        }

        private void OnDisable()
        {
            _store.Changed -= SetMoneyText;
        }

        private void SetMoneyText()
        {
            if (_countDown?.isActiveAndEnabled == true)
            {
                _countDown.Apply(_store.Money);
            }
            else
            {
                _text.SetText(_store.Money.ToString());
            }
        }
    }
}
