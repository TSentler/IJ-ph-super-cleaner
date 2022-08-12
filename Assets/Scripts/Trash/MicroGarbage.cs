using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        private void Update()
        {
            if (Target == null)
                return;

            MoveToTarget();
        }
    }
}