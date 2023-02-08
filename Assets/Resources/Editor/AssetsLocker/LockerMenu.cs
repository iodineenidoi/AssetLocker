using System.Collections.Generic;
using System.Linq;
using AssetsLocker.Api;
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
            ResponseHandler(response);
        }

        private static List<string> GetPaths()
        {
            return Selection.instanceIDs
                .Select(id => EditorUtility.InstanceIDToObject(id))
                .Select(obj => AssetDatabase.GetAssetPath(obj))
                .ToList();
        }
        
        private static void ResponseHandler(UnlockAssetsResponse response)
        {
            if (response.Unlocked.Any())
            {
                EditorUtility.DisplayDialog(
                "Success!",
                $"{response.Unlocked.Count} {(response.Unlocked.Count > 1 ? "assets were" : "asset was")} successfully unlocked",
                "Got it!");
            }
            
            foreach (string asset in response.WasntLocked)
            {
                EditorUtility.DisplayDialog(
                    "Asset was free",
                    $"\"{asset}\" wasn't locked.",
                    "Got it!");
            }
            
            foreach (AssetData asset in response.LockedByOtherUser)
            {
                EditorUtility.DisplayDialog(
                    $"Locked by \"{asset.User}\"",
                    asset.ToString(),
                    "Got it!");
            }
        }
    }
}