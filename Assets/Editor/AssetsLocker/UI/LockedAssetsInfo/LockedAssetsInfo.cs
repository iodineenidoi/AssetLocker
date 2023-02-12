using System.Collections.Generic;
using System.Linq;
using AssetsLocker.Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AssetsLocker.UI.LockedAssetsInfo
{
    public class LockedAssetsInfo : EditorWindow
    {
        private static GetAllResponse _response;
        private static string _userName;

        [MenuItem("Locker/Locked Assets Info")]
        public static async void ShowLockedAssets()
        {
            LockerApi api = new LockerApi();
            _response = await api.GetAll();
            _userName = RepositoryInformation.GetRepositoryInformation().CurrentUserName;
            
            LockedAssetsInfo wnd = GetWindow<LockedAssetsInfo>();
            wnd.titleContent = new GUIContent("Locked Assets Info");
            wnd.position = new Rect(300f, 200f, 1024f, 512f);
        }

        public void CreateGUI()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AssetsLocker/UI/LockedAssetsInfo/LockedAssetsInfo.uxml");

            VisualElement root = rootVisualElement;
            root.Add(visualTree.Instantiate());

            Button updateButton = root.Query<Button>("UpdateButton").First();
            updateButton.clickable.clicked += () => OnUpdateButtonClickedHandler(root);

            UpdateAssetsList(root);
        }

        private async void OnUpdateButtonClickedHandler(VisualElement root)
        {
            LockerApi api = new LockerApi();
            _response = await api.GetAll();
            UpdateAssetsList(root);
        }

        private void UpdateAssetsList(VisualElement root)
        {
            _response ??= new GetAllResponse
            {
                LockedAssets = new List<AssetData>()
            };

            ScrollView view = root.Query<ScrollView>("LockedAssetsView").First();
            view.Clear();
            
            if (_response.LockedAssets.Any())
            {
                string currentUser = UserHelper.GetUserName();
                
                foreach (AssetData assetData in _response.LockedAssets)
                {
                    Foldout assetDataInfoPanel = RuntimeElementsCreator.CreateAssetDataInfoPanel(assetData);
                    Button actionButton = new Button();
                    if (assetData.User == currentUser)
                    {
                        actionButton.clickable.clicked += () => UnlockAsset(assetData.Path, root);
                        actionButton.text = "Unlock Asset";
                    }
                    else
                    {
                        actionButton.clickable.clicked += () => ForceUnlockAsset(assetData.Path);
                        actionButton.text = "Force Unlock Asset";
                    }
                    assetDataInfoPanel.Add(actionButton);
                    view.Add(assetDataInfoPanel);
                }
            }
            else
            {
                view.Add(new Label("No locked assets found."));
            }
        }

        private async void UnlockAsset(string path, VisualElement root)
        {
            LockerApi api = new LockerApi();
            await api.UnlockAssets(new List<string> { path });
            OnUpdateButtonClickedHandler(root);
        }

        private void ForceUnlockAsset(string path)
        {
            
        }
    }
}