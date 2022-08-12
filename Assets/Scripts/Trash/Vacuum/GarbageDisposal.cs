using UnityEngine;
using UnityEngine.Events;

namespace Trash
{
    public class GarbageDisposal : MonoBehaviour
    {
        public event UnityAction<Garbage> OnSucked;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Sucked();
                OnSucked(garbage);
            }
        }
    }
}
