using System;
using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DeviceType = Agava.YandexGames.DeviceType;
using YaVideoAd = Agava.YandexGames.VideoAd;
using VkVideoAd = Agava.VKGames.VideoAd;
using YaInterstitialAd = Agava.YandexGames.InterstitialAd;
using VkInterstitialAd = Agava.VKGames.Interstitial;

namespace YaVk
{
    public class SocialNetwork : MonoBehaviour
    {
        private static SocialNetwork _instance;
        
        private bool _isInitialized,
            _isInitializeRun;

        public event UnityAction OnAdsStart, OnAdsEnd;
        
        public static SocialNetwork Instance => _instance;
        
        private void Awake()
        {
            if (_instance == null) 
            {
                _instance = this;
            } 
            else 
            {
                DestroyImmediate(this);
                return;
            } 
            
#if YANDEX_GAMES
            YandexGamesSdk.CallbackLogging = true;
#endif
        }

        private void Start()
        {
            TryInitializeSdk();
        }

        private void InitializeComplete()
        {
            _isInitialized = true;
            _isInitializeRun = false;
        }

        private IEnumerator TryInitializeSdk()
        {
            if (_isInitialized)
                yield break;
            
            if (_isInitializeRun)
            {
                while (_isInitialized == false && _isInitializeRun)
                {
                    yield return new WaitForSecondsRealtime(0.2f);
                }

                if (_isInitialized)
                    yield break;
            }
            _isInitializeRun = true;
#if !UNITY_WEBGL || UNITY_EDITOR
            InitializeComplete();
#elif YANDEX_GAMES
            yield return YandexGamesSdk.Initialize();
            InitializeComplete();
#elif VK_GAMES
            yield return VKGamesSdk.Initialize(
                onSuccessCallback: InitializeComplete);
#endif
        }
        
        private IEnumerator ShowInterstitialAdsCoroutine(
            UnityAction<string> onErrorCallback = null,
            UnityAction<bool> onCloseCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            yield return StartCoroutine(TryInitializeSdk());
            
#if !UNITY_WEBGL || UNITY_EDITOR
            onCloseCallback?.Invoke(true);
#elif YANDEX_GAMES
            YaInterstitialAd.Show(
                () => onYaOpenCallback?.Invoke(),
                wasShown => onCloseCallback?.Invoke(wasShown),
                error => onErrorCallback?.Invoke(error),
                () => onYaOfflineCallback?.Invoke());
#elif VK_GAMES
            VkInterstitialAd.Show(
                () => onCloseCallback?.Invoke(true),
                () =>
                {
                    onErrorCallback?.Invoke("VK interstitial ads error");
                    onCloseCallback?.Invoke(false);
                });
#endif
        }

        private IEnumerator ShowRewardedAdsCoroutine(
            UnityAction onRewardedCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onCloseCallback = null,  
            UnityAction onYaOpenCallback = null) 
        {
            yield return StartCoroutine(TryInitializeSdk());
            
#if !UNITY_WEBGL || UNITY_EDITOR
            onRewardedCallback?.Invoke();
#elif YANDEX_GAMES
            YaVideoAd.Show(() => onYaOpenCallback?.Invoke(),
                () => onRewardedCallback?.Invoke(),
                () => onCloseCallback?.Invoke(),
                error => onErrorCallback?.Invoke(error));
#elif VK_GAMES
            VkVideoAd.Show(() =>
                {
                    onRewardedCallback?.Invoke();
                    onCloseCallback?.Invoke();
                },
                () =>
                {
                    onErrorCallback?.Invoke("VK rewarded ads error");
                    onCloseCallback?.Invoke();
                });
#endif
        }
        
        public IEnumerator CheckMobileDeviceCoroutine(
            UnityAction<bool> callback)
        {
            yield return StartCoroutine(TryInitializeSdk());
            
#if YANDEX_GAMES
            callback.Invoke(Device.Type != DeviceType.Desktop);
#else
            callback.Invoke(Application.isMobilePlatform);
#endif
        }

        public void ShowInterstitialAds(UnityAction<string> onErrorCallback = null,
            UnityAction<bool> onCloseCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            OnAdsStart?.Invoke();
            onCloseCallback += wasShown => OnAdsEnd?.Invoke();
            StartCoroutine(ShowInterstitialAdsCoroutine(onErrorCallback,
                onCloseCallback, onYaOpenCallback, onYaOfflineCallback));
        }

        public void ShowRewardedAds(
            UnityAction onRewardedCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction onYaOpenCallback = null)
        {
            OnAdsStart?.Invoke();
            onCloseCallback += () => OnAdsEnd?.Invoke();
            StartCoroutine(ShowRewardedAdsCoroutine(
                onRewardedCallback, onErrorCallback, onCloseCallback,
                onYaOpenCallback));
        }
    }
}
