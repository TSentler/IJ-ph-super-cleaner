using PlayerAbilities.Move;
using UnityEngine;

namespace PlayerAbilities.Upgrade
{
    public class SpeedUpgrader : Upgrader
    {
        private readonly string _upgradeName = "SpeedUpgrade";
        
        [SerializeField] private Movement _movement;
        [Min(0f), SerializeField] private float _runSpeed;
        
        private float RunSpeed => _runSpeed + _runSpeed * UpFactor;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_movement == null)
                Debug.LogWarning("Movement was not found!", this);
        }

        protected override void Initialize()
        {
            Upgrade();
        }

        protected override void Upgrade()
        {
            _movement.Upgrade(RunSpeed);
        }

        protected override string GetUpgradeName()
        {
            return _upgradeName;
        }
    }
}
