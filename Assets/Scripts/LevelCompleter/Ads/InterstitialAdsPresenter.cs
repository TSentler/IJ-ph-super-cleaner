using System;
using UnityEngine;
using YaVk;

namespace LevelCompleter.Ads
{
    [RequireComponent(typeof(CompletePresenter))]
    public class InterstitialAdsPresenter : MonoBehaviour
    {
        private readonly SocialNetwork _socialNetwork = SocialNetwork.Instance;
        
        private CompletePresenter _completePresenter;

        private void Awake()
        {
            _completePresenter = GetComponent<CompletePresenter>();
        }

        private void OnEnable()
        {
            _completePresenter.OnInterstitialAds += InterstitialAdsHandler;
        }

        private void OnDisable()
        {
            _completePresenter.OnInterstitialAds -= InterstitialAdsHandler;
        }

        private void InterstitialAdsHandler()
        {
            _socialNetwork.ShowInterstitialAds(); 
        }
    }
}
