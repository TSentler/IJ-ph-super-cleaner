using LevelCompleter;
using UnityEngine;
using Vacuum;
using YaVk;

namespace Leaderboard
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        private SocialNetwork _socialNetwork;

        [SerializeField] private LeaderboardShowButton _button;
        [SerializeField] private LeaderboardView _view;
        [SerializeField] private Completer _completer;
        [SerializeField] private AllGarbageCollector _trash;
        
        private void OnValidate()
        {
            if (_button == null)
                Debug.LogWarning("Leaderboard Button was not found!", this);
            if (_view == null)
                Debug.LogWarning("Leaderboard View was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
            if (_trash == null)
                Debug.LogWarning("AllGarbageCollector was not found!", this);
        }
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
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
            _socialNetwork.GetLeaderboard(_trash.AllTrashRounded,
                leaderList =>
                {
                    if (_socialNetwork.IsAutoLeaderboard() || _view == null)
                        return;
                    
                    _view.ConstructLeaderboard(leaderList);
                });
        }
    }
}
