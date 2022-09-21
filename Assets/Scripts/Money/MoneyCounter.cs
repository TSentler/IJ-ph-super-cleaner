using UnityEngine;

namespace Money
{
    public class MoneyCounter : MonoBehaviour
    {
        private float _total;

        public int Total => (int)_total;

        public void Collect(float count)
        {
            _total += count;
        }
    }
}
