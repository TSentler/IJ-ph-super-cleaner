using Trash;
using UnityEngine;

namespace Vacuum
{
    [RequireComponent(typeof(SphereCollider))]
    public class GarbageSucker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Suck(transform);
            }
            
            if (other.TryGetComponent(out PhysicalEnvironment physicalEnvironment))
            {
                physicalEnvironment.Suck(transform);
            }
            
            if (other.TryGetComponent(out ISuckable simpleGarbage))
            {
                simpleGarbage.Suck();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ISuckable simpleGarbage))
            {
                simpleGarbage.Suck();
            }
            if (other.TryGetComponent(out PhysicalEnvironment physicalEnvironment))
            {
                physicalEnvironment.Suck(null);
            }
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Suck(null);
            }
        }
    }
}
