using System;
using UnityEngine;
using UnityEngine.Events;

namespace Robber
{
    public class RobberAI : MonoBehaviour
    {
        [SerializeField] private TheftTarget _theftTarget;
        [SerializeField] private Transform _exit, _carryPosition;

        public event UnityAction OnDeactivate;
        
        private void OnValidate()
        {
            if (_theftTarget == null)
                Debug.LogWarning("RobberTarget was not found!", this);
            if (_exit == null)
                Debug.LogWarning("Exit was not found!", this);
            if (_carryPosition == null)
                Debug.LogWarning("CarryPosition was not found!", this);
        }

        private void OnDisable()
        {
            OnDeactivate?.Invoke();
        }

        private Vector2 GetDirectionTo(Vector3 position)
        {
            var direction = position - transform.position;
            return new Vector2(direction.x, direction.z);
        }
        
        public Vector2 GetDirectionToTarget()
        {
            return GetDirectionTo(_theftTarget.transform.position);
        }
        
        public Vector2 GetDirectionToExit()
        {
            return GetDirectionTo(_exit.position);
        }


        public void PickUpTarget()
        {
            _theftTarget.PickUp(_carryPosition);
        }
        
        public void DropTarget()
        {
            _theftTarget.Drop();
        }
    }
}
