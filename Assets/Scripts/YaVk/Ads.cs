using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using YaInterstitialAd = Agava.YandexGames.InterstitialAd;
using VkInterstitialAd = Agava.VKGames.Interstitial;
using YaVideoAd = Agava.YandexGames.VideoAd;
using VkVideoAd = Agava.VKGames.VideoAd;

namespace YaVk
{
    public class Ads : MonoBehaviour
    {
        private float _time;
            
        [SerializeField] private Initializer _init;
        [SerializeField] private float _cooldown = 90f;

        private bool IsAdsReady => _time > _cooldown;
        
        private void Update()
        {
            _time += Time.deltaTime;
        }

        private void ResetTimer()
        {
            _time = 0f;
        }
        
        public IEnumerator ShowInterstitialAdsCoroutine(
            UnityAction<bool> onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null,
            UnityAction onYaOfflineCallback = null)
        {
            if (IsAdsReady == false)
            {
                var left = _cooldown - _time;
                onErrorCallback?.Invoke("Ads cooldown " + left + "sec");
                onCloseCallback?.Invoke(false);
                yield break;
            }
            
            yield return _init.TryInitializeSdkCoroutine();
            ResetTimer();
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

        public IEnumerator ShowRewardedAdsCoroutine(
            UnityAction onRewardedCallback = null,
            UnityAction onCloseCallback = null,
            UnityAction<string> onErrorCallback = null,
            UnityAction onYaOpenCallback = null) 
        {
            yield return StartCoroutine(_init.TryInitializeSdkCoroutine());
            ResetTimer();
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
    }
}
