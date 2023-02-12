using System.Collections.Generic;
using System.Linq;
using AssetsLocker.Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AssetsLocker.UI.UnlockAssetsResults
{
    public class UnlockAssetsResults : EditorWindow
    {
        private static UnlockAssetsResponse _response;
        
        public static void ShowResults(UnlockAssetsResponse response)
        {
            _response = response;
            
            UnlockAssetsResults wnd = GetWindow<UnlockAssetsResults>();
            wnd.titleContent = new GUIContent("Unlock Assets Results");
            wnd.position = new Rect(300f, 200f, 1024f, 512f);
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AssetsLocker/UI/UnlockAssetsResults/UnlockAssetsResults.uxml");
            root.Add(visualTree.Instantiate());

            if (_response == null)
                return;
            
            FillSimpleAssetsFoldout(root.Query<Foldout>("Foldout_Unlocked").First(), _response.Unlocked);
            FillSimpleAssetsFoldout(root.Query<Foldout>("Foldout_WasntLocked").First(), _response.WasntLocked);
            FillAssetsDataFoldout(root.Query<Foldout>("Foldout_LockedByOtherUser").First(), _response.LockedByOtherUser);
        }
        
        private void FillSimpleAssetsFoldout(Foldout root, List<string> assets)
        {
            foreach (string asset in assets)
                root.Add(RuntimeElementsCreator.CreateReadOnlyTextField(string.Empty, asset));

            root.value = assets.Any();
        }

        private void FillAssetsDataFoldout(Foldout root, List<AssetData> assets)
        {
            foreach (AssetData asset in assets)
            {
                Foldout assetDataInfoPanel = RuntimeElementsCreator.CreateAssetDataInfoPanel(asset);
                assetDataInfoPanel.style.marginLeft = new StyleLength(new Length(20f, LengthUnit.Pixel));
                root.Add(assetDataInfoPanel);
            }

            root.value = assets.Any();
        }
    }
}