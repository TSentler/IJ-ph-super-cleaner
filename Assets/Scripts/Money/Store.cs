using System;
using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class Store : MonoBehaviour
    {
        private int _money;
        
        public event UnityAction OnChange;
        
        public int Money
        {
            get => _money;
            private set => _money = value; 
        }

        public void Initialize(int money)
        {
            Money = money;
            OnChange?.Invoke();
        }
        
        public void Earn(int money)
        {
            Money += money;
            OnChange?.Invoke();
        }

        public void Buy(int coast, Action successCallback)
        {
            if (coast <= Money)
            {
                Money -= coast;
                OnChange?.Invoke();
                successCallback?.Invoke();
            } 
        }
    }
}
