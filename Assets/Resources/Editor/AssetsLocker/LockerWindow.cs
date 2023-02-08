using System.Collections.Generic;
using AssetsLocker.Api;
using UnityEditor;
using UnityEngine;

namespace AssetsLocker
{
    public class LockerWindow : EditorWindow
    {
        private static LockerWindow _currentWindow;

        private List<AssetData> _lockedAssets = new List<AssetData>();

        [MenuItem("Locker/Locked Assets Info")]
        public static void ShowWindow()
        {
            _currentWindow = GetWindow<LockerWindow>("Assets Locker");
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("Update list"))
            {
                UpdateLockedAssets();
            }

            ShowAllLockedAssets();
        }

        private async void UpdateLockedAssets()
        {
            LockerApi api = new LockerApi();
            GetAllResponse response = await api.GetAll();
            _lockedAssets = response.LockedAssets;
        }

        private void ShowAllLockedAssets()
        {
            foreach (AssetData data in _lockedAssets)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(data.ToString());
                GUILayout.EndHorizontal();
            }
        }
    }
}