using System;
using Robber;
using UnityEngine;

namespace Money
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private RobberAI _robberAI;

        private void OnValidate()
        {
            if (_robberAI == null)
                Debug.LogWarning("Robber was not found!", this);
        }

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}
