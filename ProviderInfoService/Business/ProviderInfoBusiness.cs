using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProviderInfoModel;
using ProviderInfoService.Respository;

namespace ProviderInfoService.Business
{
    public class ProviderInfoBusiness
    {
        private ProviderInfoRepository context;
        private Dictionary<string, string> paramList;
        private ProviderInfo providerInfo;

        public ProviderInfoBusiness(ProviderInfo model)
        {
            context = new ProviderInfoRepository();
            paramList = new Dictionary<string, string>();
            providerInfo = model;

            ConvertModelToParameterList();

        }
        private void ConvertModelToParameterList()
        {
            var obj = providerInfo ?? new ProviderInfo();
            paramList = paramList ?? new Dictionary<string, string>();
            foreach (var property in obj.GetType().GetProperties())
            {
                paramList.Add(property.Name, (string)property.GetValue(obj, null));
            }
        }

        public IEnumerable<ProviderInfo> GetProviderInfo()
        {
            try
            {
               return context.Get(100, paramList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
