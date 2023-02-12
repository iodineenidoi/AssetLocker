namespace AssetsLocker
{
    public static class UserHelper
    {
        private static readonly RepositoryInformation RepositoryInformation
            = RepositoryInformation.GetRepositoryInformation();
        
        public static string GetUserName()
        {
            LockerApiSettings settings = LockerApiSettings.GetInstance();

            return settings.UseGitInfo
                ? RepositoryInformation.CurrentUserName
                : settings.NotGitName;
        }

        public static string GetGitBrunch()
        {
            LockerApiSettings settings = LockerApiSettings.GetInstance();

            return settings.UseGitInfo
                ? RepositoryInformation.BranchName
                : settings.NotGitBrunch;
        }
    }
}