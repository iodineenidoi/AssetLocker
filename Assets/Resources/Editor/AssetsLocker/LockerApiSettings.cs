﻿using UnityEngine;

namespace AssetsLocker
{
    [CreateAssetMenu(menuName = "Locker Api/Settings")]
    public class LockerApiSettings : ScriptableObject
    {
        [SerializeField] private string serverUrl;
        [SerializeField] private bool useGitInfo;
        [SerializeField] private string notGitName;
        [SerializeField] private string notGitBrunch;
        
        public string ServerUrl => serverUrl;
        public bool UseGitInfo => useGitInfo;
        public string NotGitName => notGitName;
        public string NotGitBrunch => notGitBrunch;
    }
}