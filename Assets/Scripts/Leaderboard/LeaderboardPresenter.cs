using LevelCompleter;
using Saves;
using UnityEngine;
using YaVk;

namespace Leaderboard
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        private SocialNetwork _socialNetwork;
        private GameSaver _saver;

        [SerializeField] private LeaderboardShowButton _button;
        [SerializeField] private LeaderboardView _view;
        [SerializeField] private Completer _completer;
        
        private void OnValidate()
        {
            if (_saver == null)
                Debug.LogWarning("Saver was not found!", this);
        }
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
            _saver = FindObjectOfType<GameSaver>();
        }
        
        private void OnEnable()
        {
            _completer.OnComplete += LevelCompleteHandler;
            _button.OnShowBoard += ShowBoardHandler;
        }

        private void OnDisable()
        {
            _completer.OnComplete -= LevelCompleteHandler;
            _button.OnShowBoard -= ShowBoardHandler;
        }

        private void Start()
        {
            _view.gameObject.SetActive(false);
            var hasLeaderboard = _socialNetwork?.IsLeaderboardAccess() ?? false;
            _button.gameObject.SetActive(hasLeaderboard);
        }

        private void ShowBoardHandler()
        {
            if (_socialNetwork.IsAutoLeaderboard())
            {
                Apply();
            }
            else
            {
                _view.gameObject.SetActive(true);
            }
        }

        private void LevelCompleteHandler()
        {
            if (_socialNetwork.IsAutoLeaderboard())
                return; 
            
            Apply();
        }
        
        private void Apply()
        {
            _socialNetwork.GetLeaderboard(_saver.LastTrash,
                leaderList =>
                {
                    if (_socialNetwork.IsAutoLeaderboard() || _view == null)
                        return;
                    
                    _view.ConstructLeaderboard(leaderList);
                });
        }
    }
}
