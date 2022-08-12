using UnityEngine;

namespace Trash.Transforms
{
    public class LookAtRotator : MonoBehaviour
    {
        public virtual void Apply(Transform target)
        {
            transform.LookAt(target);
        }
    }
}
