using UnityEditor;
using UnityEngine;

namespace Trash
{
    [CustomEditor(typeof(GarbageSpawner))]
    class GarbageSpawnerButton : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GarbageSpawner _spawner = (GarbageSpawner)target;
            if (GUILayout.Button("Spawn"))
            {
                _spawner.SpawnInsideAllColliders();
            }
            if (GUILayout.Button("Clear"))
            {
                _spawner.Clear();
            }
        }
    }
}