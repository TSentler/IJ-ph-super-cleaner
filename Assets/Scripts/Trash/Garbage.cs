using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    public abstract class Garbage : MonoBehaviour
    {
        protected Transform _target;

        [SerializeField] protected float _speed = 200.0f;

        public event UnityAction OnSucked;
        
        public void Suck(Transform target)
        {
            _target = target;
        }
        
        public void Sucked()
        {
            gameObject.SetActive(false);
            OnSucked?.Invoke();
        }
    }
}