using UnityEngine;

namespace Audio
{
    public class AudioSwitcher : MonoBehaviour
    {
        private BackgroundAudioMuteTracker _muteTracker;
            
        [SerializeField] private AudioSwitchView _view;

        private void Awake()
        {
            _muteTracker = FindObjectOfType<BackgroundAudioMuteTracker>();
        }

        private void OnEnable()
        {
            _view.OnClickIcon += SwitchHandler;
        }

        private void OnDisable()
        {
            _view.OnClickIcon -= SwitchHandler;
        }
        
        private void Start()
        {
            _view.ChangeIcon(_muteTracker?.IsGameAudioOn ?? true);
        }

        private void SwitchHandler()
        {
            _muteTracker.SwitchGameAudio();
            _view.ChangeIcon(_muteTracker.IsGameAudioOn);
        }
    }
}
