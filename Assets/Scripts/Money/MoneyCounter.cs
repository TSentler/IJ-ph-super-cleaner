using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class MoneyCounter : MonoBehaviour
    {
        private float _total;
        private int _oldTotal;
        private bool _isPause;

        public event UnityAction OnChange;
        
        public int Total => (int)_total;

        public void Pause()
        {
            _isPause = true;
        }
        
        public void Collect(float count)
        {
            if (_isPause)
                return;
            
            _total += count;
            OnChange?.Invoke();
        }

        public void Reward()
        {
            if (_oldTotal == 0)
            {
                _oldTotal = Total;
            }

            _total += _oldTotal;
            OnChange?.Invoke();
        }
    }
}
