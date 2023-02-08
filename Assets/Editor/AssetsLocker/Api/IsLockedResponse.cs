namespace AssetsLocker.Api
{
    public class IsLockedResponse
    {
        public bool Result { get; set; }
        public AssetData Data { get; set; }
    }
}