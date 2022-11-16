using System.Collections.Generic;
using System.Linq;
using Statistics;
using UnityEngine;

namespace Trash.Statistics
{
    public class GarbageCounter : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _garbageRoots;

        private PlayerStatistics _playerStatistics;

        private void OnValidate()
        {
            if (_garbageRoots.Count == 0)
                Debug.LogWarning("GarbageRoots was not found!", this);
        }
        
        private void Awake()
        {
            _playerStatistics = FindObjectOfType<PlayerStatistics>();
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
            _playerStatistics.AddTarrgetTrashPoints(targetTrashPoints);
        }
    }
}
