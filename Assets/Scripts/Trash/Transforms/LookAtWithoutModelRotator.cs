using UnityEngine;

namespace Trash.Transforms
{
    public class LookAtWithoutModelRotator : LookAtRotator
    {
        private bool _isFirstTime = true;
        
        [SerializeField] private Transform _model;
        
        private void SetChildRoot(Transform root, Transform child)
        {
            child.parent = root.parent;
            root.parent = child;
        }
        
        public override void Apply(Transform target)
        {
            if (_isFirstTime)
            {
                SetChildRoot(transform, _model);
                base.Apply(target);
                SetChildRoot(_model, transform);
                _isFirstTime = false;
            }
            else
            {
                base.Apply(target);
            }
        }
    }
}
