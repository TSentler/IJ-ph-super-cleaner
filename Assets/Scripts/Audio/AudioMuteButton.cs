using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class AudioMuteButton : MonoBehaviour
    {
        private Button _button;
            
        [SerializeField] private Sprite _switchOn, _switchOff;
        [SerializeField] private Image _image;
        
        public event UnityAction ClickEvent;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clicked);
        }

        private void Clicked()
        {
            ClickEvent?.Invoke();
        }

        public void ChangeIcon(bool isOn)
        {
            _image.sprite = isOn ? _switchOn : _switchOff;
        }
    }
}
