using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(Animator),
        typeof(Rigidbody))]
    public class RobberSuccessfulTheftState : MonoBehaviour
    {
        private SuccessfulTheftBehaviour _successfulTheftBehaviour;
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>(); 
            _successfulTheftBehaviour = 
                _animator.GetBehaviour<SuccessfulTheftBehaviour>();
        }

        private void OnEnable()
        {
            _successfulTheftBehaviour.OnStart += SuccessfulTheftHandler;
        }

        private void OnDisable()
        {
            _successfulTheftBehaviour.OnStart -= SuccessfulTheftHandler;
        }

        private void SuccessfulTheftHandler()
        {
            gameObject.SetActive(false);
        }
    }
}
