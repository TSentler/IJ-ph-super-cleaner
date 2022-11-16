using LevelCompleter;
using Statistics;
using UnityEngine;
using YaVk;

namespace Leaderboard
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField] private LeaderboardShowButton _button;
        [SerializeField] private LeaderboardView _view;
        [SerializeField] private Completer _completer;
        
        private SocialNetwork _socialNetwork;
        private PlayerBag _playerBag;

        private void OnValidate()
        {
            if (_button == null)
                Debug.LogWarning("Leaderboard Button was not found!", this);
            if (_view == null)
                Debug.LogWarning("Leaderboard View was not found!", this);
            if (_completer == null)
                Debug.LogWarning("Completer was not found!", this);
        }
        
        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
            _playerBag = FindObjectOfType<PlayerBag>();
        }
        
        private void OnEnable()
        {
            _completer.Completed += LevelCompleted;
            _button.BoardShowed += BoardShowed;
        }

        private void OnDisable()
        {
            _completer.Completed -= LevelCompleted;
            _button.BoardShowed -= BoardShowed;
        }

        private void Start()
        {
            _view.gameObject.SetActive(false);
            var hasLeaderboard = _socialNetwork?.IsLeaderboardAccess() ?? false;
            if (hasLeaderboard)
            {
                _button.ShowCupIcon();
            }
            else
            {
                _button.ShowVacuumIcon();
            }
        }

        private void BoardShowed()
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

        private void LevelCompleted()
        {
            if (_socialNetwork.IsAutoLeaderboard())
                return; 
            
            Apply();
        }
        
        private void Apply()
        {
            _socialNetwork.GetLeaderboard(_playerBag.AllTrashPointsRounded,
                leaderList =>
                {
                    if (_socialNetwork.IsAutoLeaderboard() || _view == null)
                        return;
                    
                    _view.ConstructLeaderboard(leaderList);
                });
        }
    }
}
