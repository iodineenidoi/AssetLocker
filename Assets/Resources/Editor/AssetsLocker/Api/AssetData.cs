using System;

namespace AssetsLocker.Api
{
    public class AssetData
    {
        public string Path { get; }
        public string User { get; }
        public string Message { get; }
        public string GitBrunch { get; }
        public DateTime LockTime { get; }

        public AssetData(string path, string user, string message, string gitBrunch, DateTime lockTime)
        {
            Path = path;
            User = user;
            Message = message;
            GitBrunch = gitBrunch;
            LockTime = lockTime;
        }

        public bool IsFolder()
        {
            return Path.EndsWith("/");
        }

        public override string ToString()
        {
            return $"\"{Path}\" was locked by \"{User}\" in brunch \"{GitBrunch}\" at \"{LockTime}\" with message: \"{Message}\"";
        }
    }
}