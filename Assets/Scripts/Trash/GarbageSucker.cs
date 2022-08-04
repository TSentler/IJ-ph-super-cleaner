using UnityEngine;

namespace Trash
{
    public class GarbageSucker : MonoBehaviour
    {
        [SerializeField] private Transform _garbageDisposalCenter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Suck(_garbageDisposalCenter);
            }
        }
    }
}
