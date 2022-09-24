using UnityEngine;

namespace Trash.Boosters
{
    [RequireComponent(typeof(SphereCollider))]
    public class SuckerBooster : MonoBehaviour
    {
        private SphereCollider _sphereCollider;
        private float _radius;

        [SerializeField] private float _radiusMultiplier = 1.5f;
        
        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _radius = _sphereCollider.radius;
        }
        
        public void IncreaseSize()
        {
            _sphereCollider.radius = _radius * _radiusMultiplier;
        }

        public void ResetSize()
        {
            _sphereCollider.radius = _radius;
        }

        public void Upgrade(float radius)
        {
            _sphereCollider.radius = _radius = radius;
        }
    }
}
