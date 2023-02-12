using System.Globalization;
using AssetsLocker.Api;
using UnityEngine;
using UnityEngine.UIElements;

namespace AssetsLocker.UI
{
    public static class RuntimeElementsCreator
    {
        public static Foldout CreateAssetDataInfoPanel(AssetData data)
        {
            Foldout root = new Foldout
            {
                value = false,
                text = $"Asset: {data.Path}",
            };
            root.Add(CreateReadOnlyTextField(nameof(data.User), data.User));
            root.Add(CreateReadOnlyTextField(nameof(data.GitBrunch), data.GitBrunch));
            root.Add(CreateReadOnlyTextField(nameof(data.LockTime), data.LockTime.ToString(CultureInfo.InvariantCulture)));
            root.Add(CreateReadOnlyTextField(nameof(data.Message), data.Message));

            // Rect currentRect = root.contentRect;
            // currentRect.x = -30;
            // root.contentRect.Set(currentRect.x, currentRect.y, currentRect.width, root.contentRect.height);
            
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