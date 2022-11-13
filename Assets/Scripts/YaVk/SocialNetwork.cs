using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DeviceType = Agava.YandexGames.DeviceType;
using VkLeaderboard = Agava.VKGames.Leaderboard;


namespace YaVk
{
    [DisallowMultipleComponent,
     RequireComponent(typeof(Initializer),
        typeof(Ads))]
    public class SocialNetwork : MonoBehaviour
    {
        private Initializer _init;
        private Ads _ads;
        private YandexLeaderboard _yaLeaderboard;
        
        public event UnityAction AdsStarted, AdsEnded;

        private void Awake()
        {
            _init = GetComponent<Initializer>();
            _ads = GetComponent<Ads>();
            _yaLeaderboard = new YandexLeaderboard();
#if YANDEX_GAMES
            YandexGamesSdk.CallbackLogging = true;
#endif
        }

        private void Start()
        {
            StartCoroutine(_init.TryInitializeSdkCoroutine());
        }

        public IEnumerator CheckMobileDeviceCoroutine(
            UnityAction<bool> callback)
        {
            yield return _init.TryInitializeSdkCoroutine();

            bool isMobile = false;
#if YANDEX_GAMES
            isMobile = Device.Type != DeviceType.Desktop;
#elif VK_GAMES_MOBILE
            isMobile = true;
#else
            isMobile = Application.isMobilePlatform;
#endif
            callback.Invoke(isMobile);
        }

        public void ShowInterstitialAds(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            AdsStarted?.Invoke();
            onCloseCallback += wasShown => AdsEnded?.Invoke();
            StartCoroutine(
                _ads.ShowInterstitialAdsCoroutine(onCloseCallback, 
                    onErrorCallback, onYaOpenCallback, onYaOfflineCallback));
        }

        public void ShowRewardedAds(UnityAction onRewardedCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null)
        {
            AdsStarted?.Invoke();
            onCloseCallback += () => AdsEnded?.Invoke();
            StartCoroutine(
                _ads.ShowRewardedAdsCoroutine(onRewardedCallback, 
                    onCloseCallback, onErrorCallback, onYaOpenCallback));
        }
        
        public void GetLeaderboardPlayerEntry(
            UnityAction<LeaderboardEntryResponse> successCallback)
        {
#if !UNITY_ddEBGL || UNITY_EDITOR
            successCallback?.Invoke(null);
#elif VK_GAMES
            successCallback?.Invoke(null);
#elif YANDEX_GAMES
            _yaLeaderboard.GetLeaderboardPlayerEntry(successCallback);
#endif
        }

        public bool IsLeaderboardAccess()
        {
#if VK_GAMES && !VK_GAMES_MOBILE || CRAZY_GAMES
            return false;
#endif
            return true;
        }
        
        public bool IsAutoLeaderboard()
        {
#if VK_GAMES
            return true;
#endif
            return false;
        }
        
        public void GetLeaderboard(int score, 
            UnityAction<List<PlayerInfoLeaderboard>> successCallback)
        {
            if (IsLeaderboardAccess() == false)
                return;
            
#if !UNITY_WEBGL || UNITY_EDITOR
            _yaLeaderboard.GetTopPlayers(leaderList =>
            {
                successCallback?.Invoke(leaderList);
            }, true);
#elif VK_GAMES
            successCallback?.Invoke(new ());
            VkLeaderboard.ShowLeaderboard(score);
#elif YANDEX_GAMES
            _yaLeaderboard.AddPlayerToLeaderboard(score, () =>
            {
               _yaLeaderboard.GetTopPlayers(leaderList =>
                {
                    successCallback?.Invoke(leaderList);
                });     
            });
#endif
        }
        
        public void AddPlayerToLeaderboard(int score)
        {
#if YANDEX_GAMES && !UNITY_EDITOR
            _yaLeaderboard.AddPlayerToLeaderboard(score);
#endif
        }
    }
}
