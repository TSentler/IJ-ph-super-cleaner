using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Level
{
    public class CompletePresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private List<GameObject> _hide = new(); 
        [SerializeField] private GameObject _lvlCompletionPanel, _moneyButton;

        private void OnValidate()
        {
            if (_hide.Count == 0)
                Debug.LogWarning("Hide panels was not found!", this);
            if (_lvlCompletionPanel == null)
                Debug.LogWarning("LvlCompletionPanel was not found!", this);
        }

        private void Awake()
        {
            _lvlCompletionPanel.SetActive(false);
            _moneyButton.SetActive(false);
        }
        
        private IEnumerator ShowCoroutine()
        {
            _hide.ForEach(item => item.SetActive(false));
            _lvlCompletionPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            _moneyButton.SetActive(true);
        }

        public void SetMoney(string money)
        {
            _moneyText.SetText(money);
        }

        public void Apply()
        {
            StartCoroutine(ShowCoroutine());
        }
    }
}
