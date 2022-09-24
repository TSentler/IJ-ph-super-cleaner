using UnityEngine;
using YaVk;

namespace Money.Ads
{
    [RequireComponent(typeof(MoneyCounter))]
    public class MoneyRewardMultiplier : MonoBehaviour
    {
        private MoneyCounter _moneyCounter;

        [SerializeField] private RewardAdsButton _rewardAdsButton;
        
        private void OnValidate()
        {
            if (_rewardAdsButton == null)
                Debug.LogWarning("RewardAdsButton was not found!", this);
        }

        private void Awake()
        { 
            _moneyCounter = GetComponent<MoneyCounter>();
        }

        private void OnEnable()
        {
            _rewardAdsButton.OnReward += RewardHandler;
        }

        private void OnDisable()
        {
            _rewardAdsButton.OnReward -= RewardHandler;
        }

        private void RewardHandler()
        {
            _moneyCounter.Reward();
        }
    }
}
