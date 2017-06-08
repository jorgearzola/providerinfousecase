using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderInfoModel
{
    public class ProviderInfoResponse
    {
        public Guid MessageID  {get;set;}
        public string Message { get; set; }
        public IEnumerable<ProviderInfo> ProviderInfo { get; set; }

        public ProviderInfoResponse(IEnumerable<ProviderInfo> providerInfo)
        {
            MessageID = Guid.NewGuid();
            ProviderInfo = providerInfo;
        }
    }
}
