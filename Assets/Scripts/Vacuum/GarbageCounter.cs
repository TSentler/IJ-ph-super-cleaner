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

        private float _collectedAtLevel = 0f, _count = 0f;
        private bool _isPause;

        public int CollectedAtLevel => Mathf.RoundToInt(_collectedAtLevel);
        public int Count => Mathf.RoundToInt(_count);

        public event UnityAction<int> CountChanged;
        public event UnityAction<float> Collected;
        
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
                        _count += garbage.TrashPoints;
                    }
                }
            }
        }
        
        private void OnEnable()
        {
            _garbageDisposal.Sucked += OnSucked;
        }

        private void OnDisable()
        {
            _garbageDisposal.Sucked -= OnSucked;
        }

        private void Start()
        {
            CountChanged?.Invoke(Count);
        }
        
        private void OnSucked(Garbage garbage)
        {
            if (_isPause)
                return;
            
            _collectedAtLevel += garbage.TrashPoints;
            Collected?.Invoke(garbage.TrashPoints);
        }

        public void Pause()
        {
            _isPause = true;
        }
    }
}
