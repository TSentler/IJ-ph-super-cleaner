using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCompleter
{
    public class CompletePresenter : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _hide = new(); 
        [SerializeField] private GameObject _lvlCompletionPanel, _moneyButton;

        public event UnityAction OnInterstitialAds;
        
        private void OnValidate()
        {
            if (_hide.Count == 0)
                Debug.LogWarning("Hide panels was not found!", this);
            if (_lvlCompletionPanel == null)
                Debug.LogWarning("LvlCompletionPanel was not found!", this);
        }

        private IEnumerator ShowCoroutine()
        {
            _hide.ForEach(item => item.SetActive(false));
            _lvlCompletionPanel.SetActive(true);
            OnInterstitialAds?.Invoke();
            yield return new WaitForSeconds(1f);
            _moneyButton.SetActive(true);
        }

        public void Apply()
        {
            StartCoroutine(ShowCoroutine());
        }
    }
}
