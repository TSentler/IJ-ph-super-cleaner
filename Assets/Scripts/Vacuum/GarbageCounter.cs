using System.Collections.Generic;
using System.Linq;
using Trash;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class GarbageCounter : MonoBehaviour
    {
        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private List<GameObject> _garbageRoots;

        private float _trashPoints = 0f, _targetTrashPoints = 0f;
        private bool _isStop;

        public int TrashPoints => Mathf.RoundToInt(_trashPoints);
        public int TargetTrashPoints => Mathf.RoundToInt(_targetTrashPoints);

        public event UnityAction<int> TargetTrashPointsChanged;
        public event UnityAction<float> TrashPointsChanged;
        
        private void OnValidate()
        {
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_garbageRoots.Count == 0)
                Debug.LogWarning("GarbageRoots was not found!", this);
        }
        
        private void Awake()
        {
            foreach (var root in _garbageRoots)
            {
                var childTrash = 
                    root.GetComponentsInChildren<Garbage>();
                if (childTrash.Length != 0)
                {
                    var trash = childTrash.ToList();
                    foreach (var garbage in trash)
                    {
                        _targetTrashPoints += garbage.TrashPoints;
                    }
                }
            }
        }
        
        private void OnEnable()
        {
            _garbageDisposal.Collected += OnCollected;
        }

        private void OnDisable()
        {
            _garbageDisposal.Collected -= OnCollected;
        }

        private void Start()
        {
            TargetTrashPointsChanged?.Invoke(TargetTrashPoints);
        }
        
        private void OnCollected(Garbage garbage)
        {
            if (_isStop)
                return;
            
            _trashPoints += garbage.TrashPoints;
            TrashPointsChanged?.Invoke(garbage.TrashPoints);
        }

        public void Stop()
        {
            _isStop = true;
        }
    }
}
