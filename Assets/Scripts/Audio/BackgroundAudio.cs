using Agava.WebUtility;
using UnityEngine;
using YaVk;

namespace Audio
{
    public class BackgroundAudio : MonoBehaviour
    {
        private bool _isPlayerMuteAudio;
        
        [SerializeField] private SocialNetwork _socialNetwork;

        public bool IsGameAudioOn => AudioListener.pause == false 
                                     && AudioListener.volume > 0f;
        
        private void OnEnable()
        {
            _socialNetwork.AdsStarted += OnAdsStarted;
            _socialNetwork.AdsEnded += OnAdsEnded;
            WebApplication.InBackgroundChangeEvent += MuteAudio;
        }

        private void OnDisable()
        {
            _socialNetwork.AdsStarted -= OnAdsStarted;
            _socialNetwork.AdsEnded -= OnAdsEnded;
            WebApplication.InBackgroundChangeEvent -= MuteAudio;
        }

        private void OnAdsStarted()
        {
            MuteAudio(true);
        }

        private void OnAdsEnded()
        {
            MuteAudio(_isPlayerMuteAudio);
        }

        private void MuteAudio(bool isMute)
        {
            // Use both pause and volume muting methods at the same time.
            // They're both broken in Web, but work perfect together. Trust me on this.
            AudioListener.pause = isMute;
            AudioListener.volume = AudioListener.pause ? 0f : 1f;
        }
        
        public void SwitchGameAudio()
        {
            MuteAudio(IsGameAudioOn);
            _isPlayerMuteAudio = IsGameAudioOn == false;
        }
    }
}
