using System;
using Agava.WebUtility;
using UnityEngine;

namespace YaVk
{
    public class BackgroundAudioMuteTracker : MonoBehaviour
    {
        [SerializeField] private SocialNetwork _socialNetwork;
        
        private void OnEnable()
        {
            _socialNetwork.OnAdsStart += AdsStartHandler;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            _socialNetwork.OnAdsEnd += AdsEndHandler;
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

        private void OnInBackgroundChange(bool inBackground)
        {
            // Use both pause and volume muting methods at the same time.
            // They're both broken in Web, but work perfect together. Trust me on this.
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}