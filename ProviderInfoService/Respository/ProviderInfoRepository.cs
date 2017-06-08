using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProviderInfoModel;
using ProviderInfoService.Interface;
using RestSharp;
using System.Net;
using System.IO;
using System.Linq;

namespace ProviderInfoService.Respository
{
    public class ProviderInfoRepository : IProviderInfoRepository
    {
        
        public IEnumerable<ProviderInfo> Get(int limit, Dictionary<string, string> paramList)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://data.medicare.gov");
            var request = new RestRequest();

            AddParameters(paramList, ref request);

            request.Method = Method.GET;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            request.Resource = "resource/b27b-2uc7.json?$limit=" + limit;
            IRestResponse response = client.Execute(request);
            var list = JsonConvert.DeserializeObject<IEnumerable<ProviderInfo>>(response.Content);
            return list;
        }

        private void AddParameters(Dictionary<string, string> paramList, ref RestRequest request)
        {
            foreach (KeyValuePair<string, string> paramItem in paramList)
            {
                if (!string.IsNullOrEmpty(paramItem.Value))
                {
                    //lets support full text search
                    if (paramItem.Key == "provider_name")
                    {
                        request.AddParameter("$q", paramItem.Value);
                    }
                    else
                    {
                        request.AddParameter(paramItem.Key, paramItem.Value);
                    }
                    
                }
            }
        }
    }
}