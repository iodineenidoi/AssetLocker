using System.Collections.Generic;

namespace AssetsLocker.Api
{
    public class GetAllResponse
    {
        public List<AssetData> LockedAssets { get; set; }
    }
}