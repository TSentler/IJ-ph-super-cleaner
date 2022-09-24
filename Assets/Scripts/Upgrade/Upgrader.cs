using Money;
using UnityEngine;

namespace PlayerAbilities.Upgrade
{
    public abstract class Upgrader : MonoBehaviour
    {
        private int _upLevel;
        
        [Min(0), SerializeField] private int _coast;
        [SerializeField] private UpgradeView _view;
        [Min(0), SerializeField] private float _upFactor = 0.1f;
        [SerializeField] private Store _store;

        protected float UpFactor => _upLevel * _upFactor;
        
        protected abstract void Upgrade();
        
        private void OnValidate()
        {
            if (_view == null)
                Debug.LogWarning("UpgradeView was not found!", this);
            if (_store == null)
                Debug.LogWarning("Store was not found!", this);
        }

        private void Start()
        {
            _view.SetCoast(_coast);
        }

        private void OnEnable()
        {
            _view.OnUpgrade += UpgradeHandler;
        }

        private void OnDisable()
        {
            _view.OnUpgrade -= UpgradeHandler;
        }

        private void UpgradeHandler()
        {
            _store.Buy(_coast, () =>
            {
                _upLevel++;
                Upgrade();
            });
        }
    }
}
