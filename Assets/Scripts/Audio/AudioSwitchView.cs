using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class AudioSwitchView : MonoBehaviour
    {
        private Button _button;
            
        [SerializeField] private Sprite _switchOn, _switchOff;
        [SerializeField] private Image _image;
        
        public event UnityAction OnClickIcon;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ClickHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ClickHandler);
        }

        private void ClickHandler()
        {
            OnClickIcon?.Invoke();
        }

        public void ChangeIcon(bool isOn)
        {
            _image.sprite = isOn ? _switchOn : _switchOff;
        }
    }
}
