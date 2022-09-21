using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YaVk
{
    [RequireComponent(typeof(Button))]
    public class RewardAdsButton : MonoBehaviour
    {
        private readonly SocialNetwork _socialNetwork = SocialNetwork.Instance;

        private Button _button;
        
        public event UnityAction OnReward;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowRewardedAds);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowRewardedAds);
        }

        public void ShowRewardedAds()
        {
            _socialNetwork.ShowRewardedAds(OnReward);
        }
    }
}
