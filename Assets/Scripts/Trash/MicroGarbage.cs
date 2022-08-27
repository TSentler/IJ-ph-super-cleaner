using System.Collections;
using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        private IEnumerator SuckCoroutine()
        {
            while (Target != null)
            {
                MoveToTarget();
                yield return null;
            }
        }
        
        protected override void SuckHandler()
        {
            base.SuckHandler();
            StartCoroutine(SuckCoroutine());
        }

    }
}