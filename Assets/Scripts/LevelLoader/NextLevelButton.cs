using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelLoader
{
    [RequireComponent(typeof(Button))]
    public class NextLevelButton : MonoBehaviour
    {
        private Button _button;
        private UnityAction _nextLevelAction;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Apply);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Apply);
        }

        private void Apply()
        {
            _nextLevelAction?.Invoke();
        }

        public void SetNextLevelAction(UnityAction action)
        {
            _nextLevelAction = action;
        }
    }
}
