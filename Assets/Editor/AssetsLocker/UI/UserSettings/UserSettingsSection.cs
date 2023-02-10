using System.Collections.Generic;
using AssetsLocker;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public static class UserSettingsSection
{
    [SettingsProvider]
    public static SettingsProvider CreateUserSettingsProvider()
    {
        LockerApiSettings settings = LockerApiSettings.GetInstance();
        SerializedObject serializedObject = new SerializedObject(settings);
        
        SerializedProperty useGitInfo = serializedObject.FindProperty("useGitInfo");
        SerializedProperty overrideName = serializedObject.FindProperty("notGitName");
        SerializedProperty overrideBrunch = serializedObject.FindProperty("notGitBrunch");

        var provider = new SettingsProvider("Project/Asset Locker Api Settings", SettingsScope.User)
        {
            label = "Asset Locker Api Settings",
            keywords = new HashSet<string>(new[] { "Asset", "Locker" }),
            activateHandler = (searchContext, root) =>
            {
                VisualTreeAsset visualTree = AssetDatabase
                    .LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AssetsLocker/UI/UserSettings/UserSettingsSection.uxml");
                root.Add(visualTree.Instantiate());

                Toggle useGitInfoToggle = root.Query<Toggle>(name: "UseGitInfo").First();
                useGitInfoToggle.BindProperty(useGitInfo);

                TextField overrideNameField = root.Query<TextField>(name: "OverrideName").First();
                overrideNameField.visible = !useGitInfo.boolValue;
                overrideNameField.BindProperty(overrideName);
                
                TextField overrideBrunchField = root.Query<TextField>(name: "OverrideBrunch").First();
                overrideBrunchField.visible = !useGitInfo.boolValue;
                overrideBrunchField.BindProperty(overrideBrunch);

                useGitInfoToggle.RegisterValueChangedCallback(ev =>
                {
                    overrideNameField.visible = !ev.newValue;
                    overrideBrunchField.visible = !ev.newValue;
                });
            }
        };

        return provider;
    }
}