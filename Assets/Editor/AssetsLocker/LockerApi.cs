using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using AssetsLocker.Api;
using UnityEngine;

namespace AssetsLocker
{
    public class LockerApi
    {
        private LockerApiSettings _settings;
        
        public LockerApi()
        {
            _settings = LockerApiSettings.GetInstance();
        }

        public async void GetRandom()
        {
            using HttpClient client = new HttpClient();

            var response = await client.GetAsync($"{_settings.ServerUrl}/getRandom");
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.Log(responseString);
        }

        public async Task<GetAllResponse> GetAll()
        {
            using HttpClient client = new HttpClient();

            string project = Application.productName;

            var responseMessage = await client.GetAsync($"{_settings.ServerUrl}/getAll/{project}");
            var stringResponse = await responseMessage.Content.ReadAsStringAsync();
            
            GetAllResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllResponse>(stringResponse);
            return response;
        }

        public async Task<IsLockedResponse> IsLocked(string asset)
        {
            using HttpClient client = new HttpClient();

            IsLockedRequest request = new IsLockedRequest
            {
                Project = Application.productName,
                User = UserHelper.GetUserName(),
                Asset = asset
            };

            string contentJson = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(contentJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await client.PostAsync($"{_settings.ServerUrl}/isLocked", content);
            var stringResponse = await responseMessage.Content.ReadAsStringAsync();
            
            IsLockedResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<IsLockedResponse>(stringResponse);
            return response;
        }

        public async Task<LockAssetsResponse> LockAssets(List<string> assets, string message)
        {
            using HttpClient client = new HttpClient();
            
            LockAssetsRequest request = new LockAssetsRequest
            {
                Project = Application.productName,
                Message = message,
                User = UserHelper.GetUserName(),
                Assets = assets,
                LockTime = DateTime.Now,
                Brunch = UserHelper.GetGitBrunch()
            };

            string contentJson = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(contentJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await client.PostAsync($"{_settings.ServerUrl}/lockAssets", content);
            var stringResponse = await responseMessage.Content.ReadAsStringAsync();
            
            LockAssetsResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<LockAssetsResponse>(stringResponse);
            return response;
        }

        public async Task<UnlockAssetsResponse> UnlockAssets(List<string> assets)
        {
            using HttpClient client = new HttpClient();

            UnlockAssetsRequest request = new UnlockAssetsRequest
            {
                Project = Application.productName,
                User = UserHelper.GetUserName(),
                Assets = assets
            };

            string contentJson = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(contentJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await client.PostAsync($"{_settings.ServerUrl}/unlockAssets", content);
            var stringResponse = await responseMessage.Content.ReadAsStringAsync();

            UnlockAssetsResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<UnlockAssetsResponse>(stringResponse);
            return response;
        }
    }
}