using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trash
{
    public class GarbageSpawner : MonoBehaviour
    {
        [SerializeField] private List<BoxCollider> _boxColliders;
        [SerializeField] private GameObject _garbage;
        [SerializeField, Range(0f, 0.5f)] private float _distance, _offset;

        private void OnValidate()
        {
            if (_boxColliders.Count == 0)
            {
                Debug.LogWarning("BoxCollider list is empty!", this);
            }
        }

        private void SpawnAsGrid(Vector3 startPosition, 
            float rows, float columns, Transform parent)
        {
            var rowPosition = startPosition;
            for (int i = 0; i < rows; i++)
            {
                rowPosition -= Vector3.forward * _distance;
                for (int j = 0; j < columns; j++)
                {
                    rowPosition += Vector3.right * _distance;
                    Spawn(rowPosition, parent);
                }
                rowPosition = new Vector3(startPosition.x, 
                    rowPosition.y, rowPosition.z);
            }
        }

        private void Spawn(Vector3 position, Transform parent)
        {
            var garbage = Instantiate(_garbage);
            garbage.transform.parent = parent;
            garbage.transform.localPosition =
                position
                + Vector3.right * Random.Range(-_offset, _offset)
                + Vector3.forward * Random.Range(-_offset, _offset);

        }

        public void SpawnInsideAllColliders()
        {
            foreach (var box in _boxColliders)
            {
                var startPosition = new Vector3(
                    -box.size.x / 2,
                    box.center.y,
                    box.size.z / 2);

                var columns = box.size.x / _distance - 1f;
                var rows = box.size.z / _distance - 1f;

                SpawnAsGrid(startPosition, rows, columns, 
                    box.transform);
            }
        }
        
        public void Clear()
        {
            foreach (var box in _boxColliders)
            {
                var trash = box.GetComponentsInChildren<Garbage>();
                foreach (var garbage in trash)
                {
                    DestroyImmediate(garbage.gameObject);
                }
            }
        }
    }
}
