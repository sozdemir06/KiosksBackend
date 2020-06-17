using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICampusService
    {
         Task<List<CampusForListDto>> GetCampusListAsync();
    }
}