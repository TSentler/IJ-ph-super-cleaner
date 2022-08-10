using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Trash
{
    public class GarbageBag : MonoBehaviour
    {
        private float _collected = 0f, _count = 0f;
        
        [SerializeField] private List<Garbage> _trash = new();
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;

        private int Collected => Mathf.RoundToInt(_collected);
        private int Count => Mathf.RoundToInt(_count);
        
        private void OnValidate()
        {
            if (_trash.Count == 0)
            {
                var childTrash = GetComponentsInChildren<Garbage>();
                if (childTrash.Length != 0)
                {
                    _trash = childTrash.ToList();
                }
            }
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void OnEnable()
        {
            foreach (var garbage in _trash)
            {
                garbage.OnSucked += SuckedHandler;
            }
        }

        private void OnDisable()
        {
            foreach (var garbage in _trash)
            {
                garbage.OnSucked -= SuckedHandler;
            }
        }

        private void Start()
        {
            foreach (var garbage in _trash)
            {
                _count += garbage.Count;
            }
            _collectedText.SetCount(Count);
        }
        
        private void SuckedHandler(float count)
        {
            _collected += count;
            _collectedText.SetCollected(Collected);
            float sliderValue = (float)Collected / _count;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}