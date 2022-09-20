using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DeviceType = Agava.YandexGames.DeviceType;

namespace YaVk
{
    public class SocialNetwork : MonoBehaviour
    {
        private static SocialNetwork _instance;
        
        private bool _isInitialized,
            _isInitializeRun;

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
        
        public IEnumerator CheckMobileDeviceCoroutine(UnityAction<bool> response)
        {
            yield return StartCoroutine(TryInitializeSdk());
            
#if YANDEX_GAMES
            response.Invoke(Device.Type != DeviceType.Desktop);
#else
            response.Invoke(Application.isMobilePlatform);
#endif
        }
    }
}
