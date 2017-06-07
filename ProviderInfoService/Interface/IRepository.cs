using System.Collections.Generic;

namespace ProviderInfoService.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(int limit);
    }
}