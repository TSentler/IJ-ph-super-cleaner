using UnityEngine;

namespace Money
{
    public class MoneyCounter : MonoBehaviour
    {
        private int _total;

        public int Total => _total;

        public void Collect(int count)
        {
            _total += count;
        }
    }
}
