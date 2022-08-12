using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Trash
{
    public class GarbageCountPresenter : MonoBehaviour
    {
        private float _collected = 0f, _count = 0f;

        [SerializeField] private GarbageDisposal _garbageDisposal;
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private int Collected => Mathf.RoundToInt(_collected);
        private int Count => Mathf.RoundToInt(_count);
        
        private void OnValidate()
        {
            if (_garbageDisposal == null)
                Debug.LogWarning("GarbageDisposal was not found!", this);
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
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
            var childTrash = GetComponentsInChildren<Garbage>();
            if (childTrash.Length != 0)
            {
                var trash = childTrash.ToList();
                foreach (var garbage in trash)
                {
                    _count += garbage.Count;
                }
                _collectedText.SetCount(Count);
            }
        }
        
        private void SuckedHandler(Garbage garbage)
        {
            _collected += garbage.Count;
            _collectedText.SetCollected(Collected);
            float sliderValue = (float)Collected / _count;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}