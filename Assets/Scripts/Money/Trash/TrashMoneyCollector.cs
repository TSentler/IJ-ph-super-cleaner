using System;
using Trash;
using UnityEngine;

namespace Money.Trash
{
    [RequireComponent(typeof(GarbageDisposal))]
    public class TrashMoneyCollector : MonoBehaviour
    {
        private GarbageDisposal _garbageDisposal;

        [SerializeField] private MoneyCounter _moneyCounter;
        [Min(0f), SerializeField] private float _factor = 0.25f;
        
        private void OnValidate()
        {
            if (_moneyCounter == null)
                Debug.LogWarning("MoneyCounter was not found!", this);
        }
        
        private void Awake()
        {
            _garbageDisposal = GetComponent<GarbageDisposal>();
        }

        private void OnEnable()
        {
            _garbageDisposal.OnSucked += SuckHandler;
        }

        private void OnDisable()
        {
            _garbageDisposal.OnSucked -= SuckHandler;
        }

        private void SuckHandler(Garbage garbage)
        {
            var money = garbage.Count * _factor;
            _moneyCounter.Collect(money);
        }
    }
}
