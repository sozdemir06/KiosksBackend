using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IHomeAnnounceDal:IEntityRepository<HomeAnnounce>
    {
         Task<List<HomeAnnounce>> GetHomeAnnouncesForKiosksByScreenIdAsync(int screenId);
         Task<List<HomeAnnounce>> GetHomeAnnouncesForKiosksBySubScreenIdAsync(int subScreenId);
         Task<List<HomeAnnounce>> GetHomeAnnouncesForPublicAsync();
    }
}