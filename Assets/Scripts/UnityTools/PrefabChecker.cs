using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityTools
{
    public static class PrefabChecker
    {
        public static bool InPrefabFileOrStage(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            return InPrefabFile(checkedGameObject) || InPrefabStage();
#endif
            return false;
        }
        
        public static bool InPrefabFile(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            return PrefabUtility.GetPrefabAssetType(checkedGameObject) !=
                    PrefabAssetType.NotAPrefab
                    && IsConnectedAtScenePrefab(checkedGameObject) == false;
#endif
            return false;
        }
        
        public static bool InPrefabStage()
        {
#if UNITY_EDITOR
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            bool isValidPrefabStage = prefabStage != null && prefabStage.stageHandle.IsValid();

            return isValidPrefabStage;
#endif
            return false;
        }
        
        public static bool IsConnectedAtScenePrefab(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            var instanceStatus =
                PrefabUtility.GetPrefabInstanceStatus(checkedGameObject);
            bool prefabConnected = instanceStatus == PrefabInstanceStatus.Connected;

            return prefabConnected;
#endif
            return true;
        }

    }
}
