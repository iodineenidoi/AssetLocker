using UnityEditor;
using UnityEngine;

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

        private static LockerApiSettings _instance;
        
        public static LockerApiSettings GetInstance()
        {
            return _instance ??= AssetDatabase.LoadAssetAtPath<LockerApiSettings>("Assets/Editor/AssetsLocker/Settings/Locker Api Settings.asset");
        }
    }
}