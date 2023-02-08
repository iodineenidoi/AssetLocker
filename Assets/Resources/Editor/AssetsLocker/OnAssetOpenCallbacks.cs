using AssetsLocker.Api;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AssetsLocker
{
    [InitializeOnLoad]
    public static class OnAssetOpenCallbacks
    {
        [OnOpenAsset(1)]
        public static bool CheckIfAssetLocked(int instanceID, int line)
        {
            Object obj = EditorUtility.InstanceIDToObject(instanceID);
            string path = AssetDatabase.GetAssetPath(obj);
            WarnIfLocked(path);
            return false;
        }

        private static async void WarnIfLocked(string path)
        {
            LockerApi api = new LockerApi();
            IsLockedResponse result = await api.IsLocked(path);
            if (result.Result)
            {
                AssetData data = result.Data;
                EditorUtility.DisplayDialog(
                    "This Asset Is Locked! Any changes won't be saved!",
                    data.ToString(),
                    "Got it!");
            }
        }

        static OnAssetOpenCallbacks()
        {
            PrefabStage.prefabStageOpened += OnPrefabOpenedHandler;
        }

        private static void OnPrefabOpenedHandler(PrefabStage obj)
        {
            WarnIfLocked(obj.assetPath);
        }
    }
}