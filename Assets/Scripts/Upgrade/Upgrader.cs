using Money;
using Saves;
using UnityEngine;

namespace Upgrade
{
    public abstract class Upgrader : MonoBehaviour
    {
        private GameSaver _saver;
        private int _upLevel;
        
        [Min(-1), SerializeField] private int _upLevelOverride = -1;
        [Min(0), SerializeField] private int _coast;
        [SerializeField] private UpgradeView _view;
        [Min(0), SerializeField] private float _upFactor = 0.1f;
        [SerializeField] private Store _store;

        protected float UpFactor => _upLevel * _upFactor;

        protected abstract string GetUpgradeName();
        protected abstract void SetUpgrade();
        
        protected virtual void OnValidate()
        {
            if (_view == null)
                Debug.LogWarning("UpgradeView was not found!", this);
            if (_store == null)
                Debug.LogWarning("Store was not found!", this);
        }

        protected virtual void Awake()
        {
            _saver = FindObjectOfType<GameSaver>();
            _upLevel = _saver?.Load(GetUpgradeName()) ?? 0;
#if UNITY_EDITOR
            if (_upLevelOverride != -1)
            {
                _upLevel = _upLevelOverride;
            } 
#endif
        }

        private void Start()
        {
            _view.SetCoast(_coast);
            SetUpgrade();
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
                _saver.Save(GetUpgradeName(), _upLevel);
                SetUpgrade();
            });
        }
    }
}
