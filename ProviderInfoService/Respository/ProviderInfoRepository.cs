using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProviderInfoModel;
using ProviderInfoService.Interface;
using RestSharp;
using System.Net;
using System.IO;

namespace ProviderInfoService.Respository
{
    public class ProviderInfoRepository : IProviderInfoRepository
    {
        public IEnumerable<ProviderInfo> Get(int limit)
        {
            //var client = new RestClient();

            //client.BaseUrl = new Uri("https://data.medicare.gov");

            //var request = new RestRequest();
            //request.Method = Method.GET;
            //request.Resource = "resource/b27b-2uc7.json";
            //IRestResponse response = client.Execute(request);

            //var list = JsonConvert.DeserializeObject<IEnumerable<ProviderInfo>>(response.Content);

            //return list;

            WebRequest request = WebRequest.Create(
              "https://data.medicare.gov/resource/b27b-2uc7.json");
            // If required by the server, set the credentials.  
            //request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response. 

            //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.  
            reader.Close();
            response.Close();

            return new List<ProviderInfo>();

        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public IEnumerable<ProviderInfo> GetProviderInfo(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}