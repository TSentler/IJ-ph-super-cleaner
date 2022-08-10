using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        private void Update()
        {
            if (_target == null)
                return;

            MoveToTarget();
        }
    }
}