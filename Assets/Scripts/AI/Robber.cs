using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAbilities.Move;

namespace AI
{
    
    public class Robber : MonoBehaviour
    {
        [SerializeField] private RobberTarget _target;
        [SerializeField] private Transform _exit, _carryPosition;
        
        private void OnValidate()
        {
            if (_target == null)
                Debug.LogWarning("RobberTarget was not found!", this);
            if (_exit == null)
                Debug.LogWarning("Exit was not found!", this);
            if (_carryPosition == null)
                Debug.LogWarning("CarryPosition was not found!", this);
        }

        private Vector2 GetDirectionTo(Vector3 position)
        {
            var direction = position - transform.position;
            return new Vector2(direction.x, direction.z);
        }
        
        public Vector2 GetDirectionToTarget()
        {
            return GetDirectionTo(_target.transform.position);
        }
        
        public Vector2 GetDirectionToExit()
        {
            return GetDirectionTo(_exit.position);
        }

        public void PickUpTarget()
        {
            _target.PickUp(_carryPosition);
            
        }
    }
}
