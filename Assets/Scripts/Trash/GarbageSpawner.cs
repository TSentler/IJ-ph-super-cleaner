using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trash
{
    [RequireComponent(typeof(BoxCollider))]
    public class GarbageSpawner : MonoBehaviour
    {
        private BoxCollider _zone;
        
        [SerializeField] private GameObject _garbage;
        [SerializeField, Range(0f, 0.5f)] private float _distance, _offset;
        
        private void Awake()
        {
            _zone = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            var zoneExtents = GetZoneExtents(_zone);
            var startPosition = GetStartPoint(zoneExtents);
            var columns = zoneExtents.x * 2 / _distance - 1f;
            var rows = zoneExtents.z * 2 / _distance - 1f;
            
            Debug.Log(rows);
            Debug.Log(zoneExtents);
            
            var rowPosition = startPosition;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var garbage = Spawn(rowPosition);
                    rowPosition += Vector3.right * _distance;
                }
                rowPosition = new Vector3(startPosition.x, rowPosition.y, rowPosition.z);
                rowPosition -= Vector3.forward * _distance;
            }
        }

        private GameObject Spawn(Vector3 position)
        {
            var garbage = Instantiate(_garbage, transform);
            garbage.transform.localPosition =
                position
                + Vector3.right * Random.Range(-_offset, _offset)
                + Vector3.forward * Random.Range(-_offset, _offset);
            return garbage;
        }

        private Vector3 GetStartPoint(Vector3 zoneExtents)
        {
            var startPoint = new Vector3(
                -zoneExtents.x + _distance,
                0f,
                zoneExtents.z - _distance);
            return startPoint;
        }

        private Vector3 GetZoneExtents(BoxCollider zone)
        {
            var zoneExtents = zone.bounds.extents;
            for (int i = 0; i < 3; i++)
                zoneExtents[i] /= zone.transform.lossyScale[i];
            zoneExtents += zone.center - Vector3.one * _offset;
            return zoneExtents;
        }
    }
}
