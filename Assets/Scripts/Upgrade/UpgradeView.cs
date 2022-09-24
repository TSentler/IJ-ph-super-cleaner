using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Upgrade
{
    [RequireComponent(typeof(Button))]
    public class UpgradeView : MonoBehaviour
    {
        private Button _button;
        
        [SerializeField] private TMP_Text _coastText;

        public event UnityAction OnUpgrade;
        
        private void OnValidate()
        {
            if (_coastText == null)
                Debug.LogWarning("CoastText gameObject was not found!", this);
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(UpgradeHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UpgradeHandler);
        }

        private void UpgradeHandler()
        {
            OnUpgrade?.Invoke();
        }

        public void SetCoast(int money)
        {
            _coastText.SetText(money.ToString());
        }
    }
}
