using PlayerAbilities.Move;
using UnityEngine;
using YaVk;

namespace Tutorial
{
    public class MovementTutorial : MonoBehaviour
    {
        private SocialNetwork _socialNetwork;
        private Coroutine _checkMobileDeviceCoroutine;
        private float _minSqrMoveStep = 0.1f;
        
        [SerializeField] private GameObject _keyboardPanel,
            _stickPanel;
        [SerializeField] private Movement _movement;
        
        private void OnValidate()
        {
            if (_keyboardPanel == null)
                Debug.LogWarning("KeyboardPanel was not found!", this);
            if (_stickPanel == null)
                Debug.LogWarning("StickPanel was not found!", this);
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }

        private void Awake()
        {
            _socialNetwork = FindObjectOfType<SocialNetwork>();
            _keyboardPanel.SetActive(false);
            _stickPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _movement.OnMove += MoveTrigger;
        }

        private void OnDisable()
        {
            _movement.OnMove -= MoveTrigger;
            if (_checkMobileDeviceCoroutine !=null)
            {
                StopCoroutine(_checkMobileDeviceCoroutine);
            }
        }
        
        private void Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _keyboardPanel.SetActive(true);
            _stickPanel.SetActive(true);
            return;
#endif
            _checkMobileDeviceCoroutine = StartCoroutine(
                _socialNetwork.CheckMobileDeviceCoroutine(
                    ActivateTutorialPanel));
        }

        private void ActivateTutorialPanel(bool isMobile)
        {
            if(isMobile)
            {
                _stickPanel.SetActive(true);
            }
            else
            {
                _keyboardPanel.SetActive(true);
            }
        }
        
        private void MoveTrigger(Vector2 direction)
        {
            if (direction.sqrMagnitude < _minSqrMoveStep)
                return;

            _keyboardPanel.SetActive(false);
            _stickPanel.SetActive(false);
            enabled = false;
        }
    }
}
