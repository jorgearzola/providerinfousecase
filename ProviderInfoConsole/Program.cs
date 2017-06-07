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
            var resp = repo.Get(100);
        }
    }
}