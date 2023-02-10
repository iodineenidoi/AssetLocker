using System.Collections.Generic;
using AssetsLocker;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public static class GeneralSettingsSection
{
    [SettingsProvider]
    public static SettingsProvider CreateGeneralSettingsProvider()
    {
        LockerApiSettings settings = LockerApiSettings.GetInstance();
        SerializedObject serializedObject = new SerializedObject(settings);
        SerializedProperty serverUrl = serializedObject.FindProperty("serverUrl");

        var provider = new SettingsProvider("Project/Asset Locker Api Settings", SettingsScope.Project)
        {
            label = "Asset Locker Api Settings",
            keywords = new HashSet<string>(new[] { "Asset", "Locker" }),
            activateHandler = (searchContext, root) =>
            {
                VisualTreeAsset visualTree = AssetDatabase
                    .LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AssetsLocker/UI/GeneralSettings/GeneralSettingsSection.uxml");
                root.Add(visualTree.Instantiate());

                TextField serverAddress = root.Query<TextField>(name: "ServerAddress").First();
                serverAddress.BindProperty(serverUrl);
            }
        };

        return provider;
    }
}