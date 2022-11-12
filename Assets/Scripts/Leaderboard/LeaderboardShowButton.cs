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
        
        public event UnityAction BoardShowed;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnLeaderboardShowed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnLeaderboardShowed);
        }

        private void OnLeaderboardShowed()
        {
            BoardShowed?.Invoke();
        }
    }
}
