using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ILiveTvBroadCastDal:IEntityRepository<LiveTvBroadCast>
    {
         Task<List<LiveTvBroadCast>> GetLiveTvBroadCastForKiosksByScreenIdAsync(int screenId);
         Task<List<LiveTvBroadCast>> GetLiveTvBroadCastForKiosksBySubScreenIdAsync(int subScreenId);
    }
}