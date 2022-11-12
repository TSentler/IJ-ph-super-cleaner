using System;
using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class Store : MonoBehaviour
    {
        private int _money;
        
        public event UnityAction Changed;

        public int Money => _money;

        public void Initialize(int money)
        {
            _money = money;
            Changed?.Invoke();
        }

        public void Earn(int money)
        {
            _money += money;
            Changed?.Invoke();
        }

        public void Buy(int coast, Action successCallback)
        {
            if (coast <= _money)
            {
                _money -= coast;
                Changed?.Invoke();
                successCallback?.Invoke();
            } 
        }
    }
}
