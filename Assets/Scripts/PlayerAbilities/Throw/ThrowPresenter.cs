using UnityEngine;

namespace PlayerAbilities.Throw
{
    [RequireComponent(typeof(VacuumThrower))]
    public class ThrowPresenter : MonoBehaviour
    {
        private readonly int _isThrowPrepareHash = Animator.StringToHash("IsThrowPrepare");
        
        private VacuumThrower _vacuumThrower;
        private Vector3 _oldPosition, _oldRotation;
        
        [SerializeField] private Vector3 _throwPosition, _throwRotation;
        [SerializeField] private Transform _vacuumStickTransform;
        [SerializeField] private Animator _cleanerAnimator, _vacuumBoxAnimator;
    
        private void OnValidate()
        {
            if (_vacuumStickTransform == null)
                Debug.LogWarning("Transform was not found!", this);
            if (_cleanerAnimator == null || _vacuumBoxAnimator == null)
                Debug.LogWarning("Animator was not found!", this);
        }
        
        private void Awake()
        {
            _vacuumThrower = GetComponent<VacuumThrower>();
            _oldPosition = _vacuumStickTransform.localPosition;
            _oldRotation = _vacuumStickTransform.localRotation.eulerAngles;
        }
        
        private void OnEnable()
        {
            _vacuumThrower.OnTie += TieHandler;
            _vacuumThrower.OnBreak += BreakHandler;
        }

        private void OnDisable()
        {
            _vacuumThrower.OnTie -= TieHandler;
            _vacuumThrower.OnBreak -= BreakHandler;
        }
        
        private void TieHandler()
        {
            SetTransform(_throwPosition, _throwRotation);
            _cleanerAnimator.SetBool(_isThrowPrepareHash, true);
            _vacuumBoxAnimator.SetBool(_isThrowPrepareHash, true);
        }

        private void BreakHandler()
        {
            SetTransform(_oldPosition, _oldRotation);
            _cleanerAnimator.SetBool(_isThrowPrepareHash, false);
            _vacuumBoxAnimator.SetBool(_isThrowPrepareHash, false);
        }

        private void SetTransform(Vector3 position, Vector3 rotation)
        {
            _vacuumStickTransform.localPosition = position;
            _vacuumStickTransform.localEulerAngles = rotation;
        }
    }
}
