using UnityEngine;

namespace Audio
{
    public class AudioMuter : MonoBehaviour
    {
        private BackgroundAudio _backgroundAudio;
            
        [SerializeField] private AudioMuteButton _muteButton;

        private void Awake()
        {
            _backgroundAudio = FindObjectOfType<BackgroundAudio>();
        }

        private void OnEnable()
        {
            _muteButton.ClickEvent += AudioMuteButtonClicked;
        }

        private void OnDisable()
        {
            _muteButton.ClickEvent -= AudioMuteButtonClicked;
        }
        
        private void Start()
        {
            _muteButton.ChangeIcon(_backgroundAudio?.IsGameAudioOn ?? true);
        }

        private void AudioMuteButtonClicked()
        {
            _backgroundAudio.SwitchGameAudio();
            _muteButton.ChangeIcon(_backgroundAudio.IsGameAudioOn);
        }
    }
}
