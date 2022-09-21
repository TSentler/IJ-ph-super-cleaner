using System;
using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DeviceType = Agava.YandexGames.DeviceType;


namespace YaVk
{
    [RequireComponent(typeof(Initializer),
        typeof(Ads))]
    public class SocialNetwork : MonoBehaviour
    {
        private Initializer _init;
        private Ads _ads;
        
        public event UnityAction OnAdsStart, OnAdsEnd;

        private void OnValidate()
        {
            if (_init == null)
                Debug.LogWarning("Initializer was not found!", this);
            if (_ads == null)
                Debug.LogWarning("Ads was not found!", this);
        }

        private void Awake()
        {
            _init = GetComponent<Initializer>();
            _ads = GetComponent<Ads>();
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
            
#if YANDEX_GAMES
            callback.Invoke(Device.Type != DeviceType.Desktop);
#else
            callback.Invoke(Application.isMobilePlatform);
#endif
        }

        public void ShowInterstitialAds(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            OnAdsStart?.Invoke();
            onCloseCallback += wasShown => OnAdsEnd?.Invoke();
            StartCoroutine(
                _ads.ShowInterstitialAdsCoroutine(onCloseCallback, 
                    onErrorCallback, onYaOpenCallback, onYaOfflineCallback));
        }

        public void ShowRewardedAds(UnityAction onRewardedCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null)
        {
            OnAdsStart?.Invoke();
            onCloseCallback += () => OnAdsEnd?.Invoke();
            StartCoroutine(
                _ads.ShowRewardedAdsCoroutine(onRewardedCallback, 
                    onCloseCallback, onErrorCallback, onYaOpenCallback));
        }
    }
}
