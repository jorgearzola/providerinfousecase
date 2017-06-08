using System.Security.Cryptography.X509Certificates;

namespace ProviderInfoModel
{
    public class ProviderInfoRequest
    {
        //created as a request model in case we need it.
        public string federal_provider_number { get; set; }
        public string provider_name { get; set; }
        public string provider_zip_code { get; set; }

    }
}