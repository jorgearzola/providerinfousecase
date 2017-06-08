using System.Collections.Generic;
using ProviderInfoModel;

namespace ProviderInfoService.Interface
{
    public interface IProviderInfoRepository : IRepository<ProviderInfo>
    {
        IEnumerable<ProviderInfo> Get(int limit, Dictionary<string, string> paramList);
    }
}