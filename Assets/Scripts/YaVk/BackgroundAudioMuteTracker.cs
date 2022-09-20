using System;
using Agava.WebUtility;
using UnityEngine;

namespace YaVk
{
    [DisallowMultipleComponent]
    public class BackgroundAudioMuteTracker : MonoBehaviour
    {
        private static BackgroundAudioMuteTracker _instance;

        private void Awake()
        {
            if (_instance == null) 
            {
                _instance = this;
                DontDestroyOnLoad(this);
            } 
            else 
            {
                Destroy(this);
            } 
        }

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
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
