using System.Collections.Generic;

namespace AssetsLocker.Api
{
    public class UnlockAssetsResponse
    {
        public List<string> Unlocked { get; set; }
        public List<string> WasntLocked { get; set; }
        public List<AssetData> LockedByOtherUser { get; set; }
    }
}