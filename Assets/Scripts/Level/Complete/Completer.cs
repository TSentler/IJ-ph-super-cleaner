using Money;
using PlayerInput;
using Trash;
using UnityEngine;

namespace Level
{
    public class Completer : MonoBehaviour
    {
        [SerializeField] private MovementInput _movementInput;
        [SerializeField] private GarbageCounter _garbageCounter;
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private CompletePresenter _completePresenter;

        private void OnValidate()
        {
            if (_movementInput == null)
                Debug.LogWarning("MovementInput was not found!", this);
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completePresenter == null)
                Debug.LogWarning("CompletePresenter was not found!", this);
        }
        
        private void OnEnable()
        {
            _garbageCounter.OnCollect += CollectHandler;
        }

        private void OnDisable()
        {
            _garbageCounter.OnCollect -= CollectHandler;
        }
        
        private void CollectHandler(int collected)
        {
            if (_garbageCounter.Count == collected)
            {
                _movementInput.Pause();
                _completePresenter.SetMoney(_moneyCounter.Total.ToString());
                _completePresenter.Apply();
            }
        }
    }
}
