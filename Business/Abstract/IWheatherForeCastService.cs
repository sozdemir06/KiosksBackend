using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IWheatherForeCastService
    {
         Task<List<WheatherForeCastForReturnDto>> WheatherForeCastsAsync();
    }
}