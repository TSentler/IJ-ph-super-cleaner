using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    public class GarbageCounter : MonoBehaviour
    {
        private float _collected = 0f, _count = 0f;

        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private GameObject _garbageRoot;

        public event UnityAction<int> OnCountChange, OnCollect;
        
        private int Collected => Mathf.RoundToInt(_collected);
        
        public int Count => Mathf.RoundToInt(_count);

        private void OnValidate()
        {
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_garbageRoot == null)
                Debug.LogWarning("GarbageRoot was not found!", this);
        }
        
        private void Awake()
        {
            var childTrash = 
                _garbageRoot.GetComponentsInChildren<Garbage>();
            if (childTrash.Length != 0)
            {
                var trash = childTrash.ToList();
                foreach (var garbage in trash)
                {
                    _count += garbage.Count;
                }
            }
        }
        
        private void OnEnable()
        {
            _garbageDisposal.OnSucked += SuckedHandler;
        }

        private void OnDisable()
        {
            _garbageDisposal.OnSucked -= SuckedHandler;
        }

        private void Start()
        {
            OnCountChange?.Invoke(Count);
        }

        private void SuckedHandler(Garbage garbage)
        {
            _collected += garbage.Count;
            OnCollect?.Invoke(Collected);
        }
    }
}
