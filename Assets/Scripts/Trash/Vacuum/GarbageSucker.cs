using System;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(SphereCollider))]
    public class GarbageSucker : MonoBehaviour
    {
        [SerializeField] private GarbageDisposal _garbageDisposalCenter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISuckable garbage))
            {
                garbage.Suck(_garbageDisposalCenter);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ISuckable environment))
            {
                environment.Suck(null);
            }
        }
    }
}
