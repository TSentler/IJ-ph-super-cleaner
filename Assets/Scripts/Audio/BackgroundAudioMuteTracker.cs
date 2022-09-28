using Agava.WebUtility;
using UnityEngine;
using YaVk;

namespace Audio
{
    public class BackgroundAudioMuteTracker : MonoBehaviour
    {
        private bool _isBackground;
        
        [SerializeField] private SocialNetwork _socialNetwork;

        public bool IsGameAudioOn { get; private set; } = true;
        
        private void OnEnable()
        {
            _socialNetwork.OnAdsStart += AdsStartHandler;
            _socialNetwork.OnAdsEnd += AdsEndHandler;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            _socialNetwork.OnAdsStart -= AdsStartHandler;
            _socialNetwork.OnAdsEnd -= AdsEndHandler;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void AdsStartHandler()
        {
            OnInBackgroundChange(true);
        }

        private void AdsEndHandler()
        {
            OnInBackgroundChange(false);
        }

        private void OnInBackgroundChange(bool isBack)
        {
            _isBackground = isBack;
            SwitchAudio();
        }

        private void SwitchAudio()
        {
            var isAudioOn = _isBackground == false && IsGameAudioOn;
            // Use both pause and volume muting methods at the same time.
            // They're both broken in Web, but work perfect together. Trust me on this.
            AudioListener.pause = isAudioOn == false;
            AudioListener.volume = isAudioOn ? 1f : 0f;
        }

        public void SwitchGameAudio()
        {
            IsGameAudioOn = !IsGameAudioOn;
            SwitchAudio();
        }
    }
}
