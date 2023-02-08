namespace AssetsLocker.Api
{
    public class IsLockedRequest
    {
        public string Project { get; set; }
        public string User { get; set; }
        public string Asset { get; set; }
    }
}