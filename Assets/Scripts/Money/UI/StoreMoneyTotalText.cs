using TMPro;
using UI;
using UnityEngine;

namespace Money.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StoreMoneyTotalText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private CountDown _countDown;

        [SerializeField] private Store _store;

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
            _store.OnChange += SetMoneyText;
        }

        private void OnDisable()
        {
            _store.OnChange -= SetMoneyText;
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
