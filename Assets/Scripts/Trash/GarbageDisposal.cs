using UnityEngine;

namespace Trash
{
    public class GarbageDisposal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Sucked();
            }
        }
    }
}
