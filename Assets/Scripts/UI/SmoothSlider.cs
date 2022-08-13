using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class SmoothSlider : MonoBehaviour
    {
        private Slider _slider;
        private Coroutine _slideCoroutine;
        private float _previous, _target;

        [SerializeField] private float _duration = 1f; 
    
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = 0f;
            _slider.maxValue = 1f;
            _slider.value = _slider.minValue;
        }
    
        private IEnumerator SlideCoroutine()
        {
            var elapsed = 0f;
            while (elapsed < _duration)
            {
                _slider.value = Mathf.Lerp(_previous, _target, 
                    elapsed / _duration);
                yield return null;
                elapsed += Time.deltaTime;
            }

            _slider.value = _target;
            _slideCoroutine = null;
        }
    
        public void SetValue(float value)
        {
            if (_slideCoroutine != null)
                StopCoroutine(_slideCoroutine);

            _previous = _slider.value;
            _target = value;
            _slideCoroutine = StartCoroutine(SlideCoroutine());
        }
    }
}
