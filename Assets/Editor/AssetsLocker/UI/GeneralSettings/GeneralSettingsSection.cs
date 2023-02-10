using System.Collections.Generic;
using AssetsLocker;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public static class GeneralSettingsSection
{
    // [MenuItem("Window/UI Toolkit/GeneralSettingsSection")]
    // public static void ShowExample()
    // {
    //     GeneralSettingsSection wnd = GetWindow<GeneralSettingsSection>();
    //     wnd.titleContent = new GUIContent("GeneralSettingsSection");
    // }

    // public void CreateGUI()
    // {
    //     // Each editor window contains a root VisualElement object
    //     VisualElement root = rootVisualElement;
    //
    //     // VisualElements objects can contain other VisualElement following a tree hierarchy.
    //     VisualElement label = new Label("Hello World! From C#");
    //     root.Add(label);
    //
    //     // Import UXML
    //     var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AssetsLocker/UI/GeneralSettings/GeneralSettingsSection.uxml");
    //     VisualElement labelFromUXML = visualTree.Instantiate();
    //     root.Add(labelFromUXML);
    //
    //     // A stylesheet can be added to a VisualElement.
    //     // The style will be applied to the VisualElement and all of its children.
    //     var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/AssetsLocker/UI/GeneralSettings/GeneralSettingsSection.uss");
    //     VisualElement labelWithStyle = new Label("Hello World! With Style");
    //     labelWithStyle.styleSheets.Add(styleSheet);
    //     root.Add(labelWithStyle);
    // }
    
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
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