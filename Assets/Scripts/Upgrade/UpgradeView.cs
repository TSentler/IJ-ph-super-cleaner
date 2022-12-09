using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Upgrade
{
    [RequireComponent(typeof(Button))]
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coastText;
        [SerializeField] private UpLevelText _upLevelText;

        private Button _button;
        
        public event UnityAction Upgraded;
        
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
            _button.onClick.AddListener(OnUpgraded);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnUpgraded);
        }

        private void OnUpgraded()
        {
            Upgraded?.Invoke();
        }

        public void Setup(int level, int money)
        {
            _coastText.SetText(money.ToString());
            _upLevelText.SetLevel(level);
        }
    }
}
