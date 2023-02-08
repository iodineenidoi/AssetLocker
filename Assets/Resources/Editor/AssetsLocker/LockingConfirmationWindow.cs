﻿using System.Collections.Generic;
using System.Linq;
using AssetsLocker.Api;
using UnityEditor;
using UnityEngine;

namespace AssetsLocker
{
    public class LockingConfirmationWindow : EditorWindow
    {
        private static LockingConfirmationWindow _window;
        private static List<Object> _objects;
        private static RepositoryInformation _repositoryInformation;
        private static LockerApiSettings _settings;
        private static string _currentUserName;
        private static string _currentBranchName;

        private string _userMessage = string.Empty;

        public static void Open(int[] instancesIDs)
        {
            _window = CreateWindow<LockingConfirmationWindow>();
            _window.position = new Rect(200, 200, 500, 500);
            
            _objects = instancesIDs.Select(EditorUtility.InstanceIDToObject).ToList();
            _repositoryInformation = RepositoryInformation.GetRepositoryInformation();
            _settings = Resources.Load<LockerApiSettings>("Editor/AssetsLocker/Locker Api Settings");

            _currentUserName = _settings.UseGitInfo
                ? _repositoryInformation.CurrentUserName
                : _settings.NotGitName;
            
            _currentBranchName = _settings.UseGitInfo
                ? _repositoryInformation.BranchName
                : _settings.NotGitBrunch;
        }

        private void OnGUI()
        {
            GUILayout.Label($"Current User: {_currentUserName}");
            GUILayout.Label($"Current Git Branch: {_currentBranchName}");
            
            GUILayout.Space(10f);
            GUILayout.Label("Selected objects to lock:");
            for (int i = 0; i < _objects.Count; i++)
                _objects[i] = EditorGUILayout.ObjectField(_objects[i], typeof(Object), false);

            if (GUILayout.Button("Add below"))
                _objects.Add(null);

            if (_objects != null && _objects.Any() && GUILayout.Button("Remove last"))
                _objects.Remove(_objects.Last());

            GUILayout.Space(10f);
            GUILayout.Label("Please, enter message for other developers to lock selected assets:");
            _userMessage = GUILayout.TextField(_userMessage);

            if (!string.IsNullOrWhiteSpace(_userMessage) && GUILayout.Button("Confirm"))
            {
                Confirm();
                _userMessage = string.Empty;
            }
        }

        private async void Confirm()
        {
            List<string> paths = new List<string>();
            foreach (Object obj in _objects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (AssetDatabase.IsValidFolder(path))
                {
                    path += "/";
                }
                paths.Add(path);
            }
                
            LockerApi api = new LockerApi();
            LockAssetsResponse response = await api.LockAssets(paths, _userMessage);
            ResponseHandler(response);
                
            _window.Close();
            _window = null;
        }

        private void ResponseHandler(LockAssetsResponse response)
        {
            if (response.Saved.Any())
            {
                EditorUtility.DisplayDialog(
                    "Success!",
                    $"{response.Saved.Count} {(response.Saved.Count > 1 ? "assets were" : "asset was")} successfully locked",
                    "Got it!");
            }
            else
            {
                EditorUtility.DisplayDialog(
                    "Failed!",
                    "No asset was locked for editing!",
                    "Got it!");
            }

            foreach (AssetData asset in response.Failed)
            {
                EditorUtility.DisplayDialog(
                    "Can't lock the asset.",
                    asset.ToString(),
                    "Got it!");
            }
        }
        
    }
}