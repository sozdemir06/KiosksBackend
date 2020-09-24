using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAnnounceDal:IEntityRepository<Announce>
    {
         Task<List<Announce>> GetAnnounceForKiosksByScreenIdAsync(int screenId);
         Task<List<Announce>> GetAnnounceForKiosksBySubScreenIdAsync(int subScreenId);
         Task<List<Announce>> GetAnnounceForPublicAsync();
    }
}