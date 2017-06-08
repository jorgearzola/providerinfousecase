using System;
using System.Collections.Generic;
using ProviderInfoService;
using ProviderInfoModel;
using ProviderInfoService.Respository;


namespace ProviderInfoConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ProviderInfoRepository repo = new ProviderInfoRepository();
            var paramlist = new Dictionary<string, string>();
            //paramlist.Add("provider_name", "");
            paramlist.Add("provider_zip_code", "35150");
            //paramlist.Add("provider_phone_number", "");
            //paramlist.Add("federal_provider_number", "");
            var resp = repo.Get(100, paramlist);
        }
    }
}