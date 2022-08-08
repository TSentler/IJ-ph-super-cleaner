using System.Collections.Generic;
using UnityEngine;

namespace Trash
{
    [RequireComponent(typeof(GarbageBag))]
    public class MicroGarbageEditorAutoSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _microGarbageRoot;
        [SerializeField] private MicroGarbageSpawner _spawner;
        [Min(1), SerializeField] private int _countMicroGarbage = 1;

        private void OnValidate()
        {
            if (_microGarbageRoot == null)
                Debug.LogWarning("MicroGarbageRoot was not found!", this);
            if (_spawner == null)
                Debug.LogWarning("MicroGarbageSpawner was not found!", this);
        }

        public void SpawnButton()
        {
            List<MicroGarbage> trash = _spawner.SpawnInsideAllColliders();
            
            var oneGarbageCount = (float)_countMicroGarbage / trash.Count;
            foreach (var garbage in trash)
            {
                garbage.SetCount(oneGarbageCount);
            }
        }        
        
        public void ClearButton()
        {
            var trash = _microGarbageRoot.GetComponentsInChildren<MicroGarbage>();
            foreach (var garbage in trash)
            {
                DestroyImmediate(garbage.gameObject);
            }
        }
    }
}
