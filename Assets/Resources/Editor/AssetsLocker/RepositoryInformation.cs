using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Windows;

namespace AssetsLocker
{
    /// <summary>
    /// Copied from https://blog.somewhatabstract.com/2015/06/22/getting-information-about-your-git-repository-with-c/
    /// With some changes
    /// </summary>
    
    public class RepositoryInformation : IDisposable
    {
        private bool _disposed;
        private readonly Process _gitProcess;
        
        public static RepositoryInformation GetRepositoryInformation()
        {
            string path = GetProjectPath();
            string gitPath = GetGitPath();
            
            var repositoryInformation = new RepositoryInformation(path, gitPath);
            if (repositoryInformation.IsGitRepository)
            {
                return repositoryInformation;
            }

            return null;
        }

        public string BranchName
        {
            get { return RunCommand("rev-parse --abbrev-ref HEAD"); }
        }

        public string CurrentUserName
        {
            get { return RunCommand("config user.name"); }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _gitProcess.Dispose();
            }
        }

        private RepositoryInformation(string path, string gitPath)
        {
            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = Directory.Exists(gitPath) ? gitPath : "git.exe",
                CreateNoWindow = true,
                WorkingDirectory = (path != null && Directory.Exists(path)) ? path : Environment.CurrentDirectory
            };

            _gitProcess = new Process();
            _gitProcess.StartInfo = processInfo;
        }

        private bool IsGitRepository
        {
            get { return !String.IsNullOrWhiteSpace(RunCommand("log -1")); }
        }

        private string RunCommand(string args)
        {
            _gitProcess.StartInfo.Arguments = args;
            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
            return output;
        }

        private static string GetProjectPath()
        {
            string path = Application.dataPath;
            string assets = "/Assets";
            path = path.Remove(path.Length - assets.Length);
            
            return path;
        }

        private static string GetGitPath()
        {
            // todo: add finding a real git path if git.exe is not in the PATH
            return "git.exe";
        }
    }
}