using System;
using PlayerAbilities.Move;
using UnityEngine;

namespace Tutorial
{
    public class MovementTutorial : MonoBehaviour
    {
        float minSqrMoveStep = 0.1f;
        
        [SerializeField] private GameObject _keyboardPanel,
            _stickPanel;
        [SerializeField] private Movement _movement;
        
        private void OnValidate()
        {
            if (_keyboardPanel == null)
                Debug.LogWarning("KeyboardPanel was not found!", this);
            if (_stickPanel == null)
                Debug.LogWarning("StickPanel was not found!", this);
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }

        private void Awake()
        {
            _keyboardPanel.SetActive(true);
            _stickPanel.SetActive(true);
        }

        private void OnEnable()
        {
            _movement.OnMove += MoveTrigger;
        }

        private void OnDisable()
        {
            _movement.OnMove -= MoveTrigger;
        }

        private void MoveTrigger(Vector2 direction)
        {
            if (direction.sqrMagnitude < minSqrMoveStep)
                return;

            _keyboardPanel.SetActive(false);
            _stickPanel.SetActive(false);
            enabled = false;
        }
    }
}
