using System.Globalization;
using AssetsLocker.Api;
using UnityEditor;
using UnityEngine.UIElements;

namespace AssetsLocker.UI
{
    public static class RuntimeElementsCreator
    {
        private static VisualTreeAsset _assetDataView = null;
        
        public static Foldout CreateAssetDataInfoPanel(AssetData data)
        {
            if (_assetDataView == null)
            {
                _assetDataView = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AssetsLocker/UI/Shared/AssetDataView.uxml");
            }

            Foldout root = _assetDataView.CloneTree().Query<Foldout>("AssetDataRoot").First();
            root.value = false;
            root.text = $"<b>Asset: {data.Path}</b>";
            root.Query<TextField>("User").First().value = data.User;
            root.Query<TextField>("GitBrunch").First().value = data.GitBrunch;
            root.Query<TextField>("LockTime").First().value = data.LockTime.ToString(CultureInfo.InvariantCulture);
            root.Query<TextField>("Message").First().value = data.Message;

            return root;
        }
        
        public static TextField CreateReadOnlyTextField(string label, string value, bool multiline = false)
        {
            return new TextField
            {
                isReadOnly = true,
                value = value,
                label = label,
                multiline = multiline,
            };
        }
    }
}