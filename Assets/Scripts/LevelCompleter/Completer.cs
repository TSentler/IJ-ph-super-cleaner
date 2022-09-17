using Money;
using PlayerInput;
using UnityEngine;

namespace LevelCompleter
{
    public class Completer : MonoBehaviour
    {
        private bool _isCompleted;
        
        [SerializeField] private MovementInput _movementInput;
        [SerializeField] private MoneyCounter _moneyCounter;
        [SerializeField] private CompletePresenter _completePresenter;

        private void OnValidate()
        {
            if (_movementInput == null)
                Debug.LogWarning("MovementInput was not found!", this);
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
            if (_completePresenter == null)
                Debug.LogWarning("CompletePresenter was not found!", this);
        }

        public void Complete()
        {
            if (_isCompleted)
                return;
            
            _isCompleted = true;
            _movementInput.Pause();
            _completePresenter.SetMoney(_moneyCounter.Total.ToString());
            _completePresenter.Apply();
        }
    }
}
