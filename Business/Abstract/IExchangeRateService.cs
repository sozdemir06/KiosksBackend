using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IExchangeRateService
    {
         Task<List<ExchangeRate>> GetExChangeRateAsync();
    }
}