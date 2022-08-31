using System;
using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class MoneyCounter : MonoBehaviour
    {
        private float _total;

        public void Collect(int count)
        {
            _total += count;
        }
    }
}
