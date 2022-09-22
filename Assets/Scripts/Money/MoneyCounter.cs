using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class MoneyCounter : MonoBehaviour
    {
        private float _total;
        private int _oldTotal;
        private bool _isPause;

        public event UnityAction<int> OnCollect;
        
        public int LevelTotal => (int)_total;

        public void Pause()
        {
            _isPause = true;
        }
        
        public void Collect(float count)
        {
            if (_isPause)
                return;
            
            _total += count;
            OnCollect?.Invoke(LevelTotal);
        }

        public void Reward()
        {
            if (_oldTotal == 0)
            {
                _oldTotal = LevelTotal;
            }

            _total += _oldTotal;
            OnCollect?.Invoke(_oldTotal);
        }
    }
}
