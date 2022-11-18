using System.Collections.Generic;
using System.Linq;
using Trash;
using UnityEngine;
using UnityEngine.Events;

namespace Vacuum
{
    public class GarbageCounter : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _garbageRoots;

        private float _targetTrashPoints = 0f;
        
        public int TargetTrashPoints => Mathf.RoundToInt(_targetTrashPoints);
        
        public event UnityAction TargetTrashPointsChanged;
        
        private void OnValidate()
        {
            if (_garbageRoots.Count == 0)
                Debug.LogWarning("GarbageRoots was not found!", this);
        }
        
        private void Awake()
        {
            var targetTrashPoints = 0f;
            foreach (var root in _garbageRoots)
            {
                var childTrash = 
                    root.GetComponentsInChildren<Garbage>();
                if (childTrash.Length != 0)
                {
                    var trash = childTrash.ToList();
                    foreach (var garbage in trash)
                    {
                        targetTrashPoints += garbage.TrashPoints;
                    }
                }
            }
            _targetTrashPoints += targetTrashPoints;
            TargetTrashPointsChanged?.Invoke();
        }
    }
}
