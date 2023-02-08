using System.Collections.Generic;

namespace AssetsLocker.Api
{
    public class LockAssetsResponse
    {
        public List<string> Saved { get; set; }
        public List<AssetData> Failed { get; set; }
    }
}