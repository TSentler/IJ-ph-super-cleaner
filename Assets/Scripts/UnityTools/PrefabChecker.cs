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
            return InPrefabStage() || InPrefabFile(checkedGameObject);
#endif
            return true;
        }
        
        public static bool InPrefabStage()
        {
#if UNITY_EDITOR
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            bool isValidPrefabStage = prefabStage != null && prefabStage.stageHandle.IsValid();

            return isValidPrefabStage;
#endif
            return true;
        }
        
        public static bool InPrefabFile(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            bool prefabConnected = 
                PrefabUtility.GetPrefabInstanceStatus(checkedGameObject) == PrefabInstanceStatus.Connected;

            return prefabConnected == false;
#endif
            return true;
        }
        
    }
}
