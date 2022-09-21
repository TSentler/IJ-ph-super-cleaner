using UnityEngine;
using Agava.VKGames;
using UnityEngine.UI;
using System.Collections;

namespace Agava.VKGames.Samples
{
    public class PlaytestingCanvasVk : MonoBehaviour
    {
        [SerializeField] private Text _coinsAmountText;

        private int _coinsAmount = 0;

        public void InviteFriendsButton()
        {
            SocialInteraction.InviteFriends(OnRewardedCallback);
        }

        public void AddPlayerToCommunity()
        {
            Community.InviteToIJuniorGroup(OnRewardedCallback);
        }

        private void OnRewardedCallback()
        {
            _coinsAmount += 40;
            _coinsAmountText.text = $"{_coinsAmount} coins";
        }

        public void ShowLeaderboardButton()
        {
            Leaderboard.ShowLeaderboard(100);
        }
    }
}
