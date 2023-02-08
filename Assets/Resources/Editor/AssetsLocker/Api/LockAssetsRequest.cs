using System;
using System.Collections.Generic;

namespace AssetsLocker.Api
{
    public class LockAssetsRequest
    {
        public string Project { get; set; }
        public string User { get; set; }
        public string Brunch { get; set; }
        public string Message { get; set; }
        public DateTime LockTime { get; set; }
        public List<string> Assets { get; set; }
    }
}