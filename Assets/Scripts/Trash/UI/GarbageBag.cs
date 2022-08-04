using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Trash
{
    public class GarbageBag : MonoBehaviour
    {
        private int _collected = 0;
        
        [SerializeField] private List<Garbage> _childTrash = new();
        [SerializeField] private CollectedText _collectedText;
        [SerializeField] private SmoothSlider _collectedSlider;
        
        private void OnValidate()
        {
            if (_childTrash.Count == 0)
            {
                var childTrash = GetComponentsInChildren<Garbage>();
                if (childTrash.Length != 0)
                {
                    _childTrash = childTrash.ToList();
                }
            }
            if (_collectedText == null)
                Debug.LogWarning("CollectedText was not found!", this);
            if (_collectedSlider == null)
                Debug.LogWarning("SmoothSlider was not found!", this);
        }

        private void OnEnable()
        {
            foreach (var garbage in _childTrash)
            {
                garbage.OnSucked += SuckedHandler;
            }
        }

        private void OnDisable()
        {
            foreach (var garbage in _childTrash)
            {
                garbage.OnSucked -= SuckedHandler;
            }
        }

        private void Start()
        {
            _collectedText.SetCount(_childTrash.Count);
        }

        private void SuckedHandler()
        {
            _collected++;
            _collectedText.SetCollected(_collected);
            float sliderValue = (float)_collected / _childTrash.Count;
            _collectedSlider.SetValue(sliderValue);
        }
    }
}