using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(Camera), 
        typeof(Follow), 
        typeof(Zoom))]
    public class MultiplyTarget : MonoBehaviour
    {
        private Follow _follow;
        private Zoom _zoom;
        
        [SerializeField] private List<Transform> _targets = new();

        private void OnValidate()
        {
            if (_targets.Count == 0)
                Debug.LogWarning("_targets was not found!", this);
        }

        private void Awake()
        {
            _follow = GetComponent<Follow>();
            _zoom = GetComponent<Zoom>();
        }

        private void LateUpdate()
        {
            if (_targets.Count == 0)
                return;

            var bounds = GetBounds();
            _follow.Apply(bounds.center);
            _zoom.Apply(bounds.size.x);
        }

        private Bounds GetBounds()
        {
            var bounds = new Bounds(_targets[0].position, Vector3.zero);
            for (int i = 1; i < _targets.Count; i++)
            {
                if (_targets[i].gameObject.activeSelf)
                {
                    bounds.Encapsulate(_targets[i].position);
                }
            }

            return bounds;
        }
    }
}
