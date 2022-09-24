using Trash.Boosters;
using UnityEngine;

namespace Upgrade
{
    public class DistanceUpgrader : Upgrader
    {
        private readonly string _upgradeName = "DistanceUpgrader";
        
        [SerializeField] private SuckerBooster _suckerBooster;
        [Min(0f), SerializeField] private float _radius;
        
        private float Radius => _radius + _radius * UpFactor;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_suckerBooster == null)
                Debug.LogWarning("SuckerBooster was not found!", this);
        }

        protected override void SetUpgrade()
        {
            _suckerBooster.Upgrade(Radius);
        }

        protected override string GetUpgradeName()
        {
            return _upgradeName;
        }
    }
}
