using System.Collections.Generic;

namespace AssetsLocker.Api
{
    public class UnlockAssetsRequest
    {
        public string Project { get; set; }
        public string User { get; set; }
        public List<string> Assets { get; set; }
    }
}