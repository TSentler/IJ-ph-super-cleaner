using Robber;
using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    [RequireComponent(typeof(Animator))]
    public class RobberMoneyCollector : MonoBehaviour
    {
        private Animator _animator;
        private SuckBehaviour _suckBehaviour;

        [SerializeField] private GameObject _money;
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private int _count;

        private void OnValidate()
        {
            if (_money == null)
                Debug.LogWarning("Money was not found!", this);
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _suckBehaviour = _animator.GetBehaviour<SuckBehaviour>();
        }

        private void OnEnable()
        {
            _suckBehaviour.OnSuckStart += SuckHandler;
        }

        private void OnDisable()
        {
            _suckBehaviour.OnSuckStart -= SuckHandler;
        }
        
        private void SuckHandler()
        {
            _moneyCounter.Collect(_count);
            _money.transform.parent = null;
            _money.SetActive(true);
        }
    }
}
