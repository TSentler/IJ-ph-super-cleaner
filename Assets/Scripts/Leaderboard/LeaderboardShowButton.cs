using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YaVk;

namespace Leaderboard
{
    [RequireComponent(typeof(Button))]
    public class LeaderboardShowButton : MonoBehaviour
    {
        private Button _button;
        
        public event UnityAction OnShowBoard;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowLeaderboard);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowLeaderboard);
        }

        private void ShowLeaderboard()
        {
            OnShowBoard?.Invoke();
        }
    }
}
