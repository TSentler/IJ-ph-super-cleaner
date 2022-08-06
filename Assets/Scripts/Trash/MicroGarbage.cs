using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        private void Update()
        {
            if (_target == null)
                return;

            var deltaSpeed = _speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, _target.position, deltaSpeed);

            if (Vector3.Distance(transform.position, _target.position) < 0.001f)
            {
                transform.position = _target.position;
            }
        }
    }
}