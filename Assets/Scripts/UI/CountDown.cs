using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CountDown : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        
        [Min(0f), SerializeField] private float _time = 0.3f;

        public UnityEvent OnCountDown, OnEnd;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            if (int.TryParse(_text.text, out var number) == false)
                Debug.LogWarning("Text is not integer!", this);
        }

        private IEnumerator CountDownCoroutine(int number)
        {
            var elapsed = 0f;
            while (elapsed < _time)
            {
                elapsed += Time.deltaTime;
                var rate = elapsed / _time;
                var i = (int)Mathf.Lerp(number, 0f, rate);
                _text.SetText(i.ToString());
                yield return null;
            }
            
            OnEnd?.Invoke();
        }

        public void Apply()
        {
            if (int.TryParse(_text.text, out var number)
                && number > 0)
            {
                OnCountDown?.Invoke();
                StartCoroutine(CountDownCoroutine(number));
            }
            else
            {
                OnEnd?.Invoke();
            }
        }
    }
}
