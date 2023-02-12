using System.Collections.Generic;
using System.Linq;
using AssetsLocker.Api;
using AssetsLocker.UI.UnlockAssetsResults;
using UnityEditor;
using UnityEngine;

namespace AssetsLocker
{
    public class LockerMenu
    {
        [MenuItem("Assets/Lock Asset")]
        public static void LockAsset()
        {
            LockingConfirmationWindow.Open(Selection.instanceIDs);
        }

        [MenuItem("Assets/Unlock Asset")]
        public static async void UnlockAsset()
        {
            List<string> paths = GetPaths();
            for (int i = 0; i < paths.Count; i++)
                if (AssetDatabase.IsValidFolder(paths[i]))
                    paths[i] += "/";

            LockerApi api = new LockerApi();
            UnlockAssetsResponse response = await api.UnlockAssets(paths);
            UnlockAssetsResults.ShowResults(response);
        }

        private static List<string> GetPaths()
        {
            return Selection.instanceIDs
                .Select(id => EditorUtility.InstanceIDToObject(id))
                .Select(obj => AssetDatabase.GetAssetPath(obj))
                .ToList();
        }
    }
}