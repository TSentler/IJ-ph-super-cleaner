using UnityEngine;

namespace Robber
{
    [RequireComponent(typeof(Animator))]
    public class StumbleState : MonoBehaviour
    {
        [SerializeField] private RobberAI _robberAI;
        
        private StumbleBehaviour _stumbleBehaviour;
        private Animator _animator;
        
        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _stumbleBehaviour = 
                _animator.GetBehaviour<StumbleBehaviour>();
        }

        private void OnEnable()
        {
            _stumbleBehaviour.OnStart += StumbleStartHandler;
        }

        private void OnDisable()
        {
            _stumbleBehaviour.OnStart -= StumbleStartHandler;
        }

        private void StumbleStartHandler()
        {
            _robberAI.DropTarget();
        }
    }
}
