using System.Globalization;
using AssetsLocker.Api;
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
                text = $"<b>Asset: {data.Path}</b>",
            };
            
            root.Add(CreateReadOnlyTextField(nameof(data.User), data.User));
            root.Add(CreateReadOnlyTextField(nameof(data.GitBrunch), data.GitBrunch));
            root.Add(CreateReadOnlyTextField(nameof(data.LockTime), data.LockTime.ToString(CultureInfo.InvariantCulture)));
            root.Add(CreateReadOnlyTextField(nameof(data.Message), data.Message));

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