using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Helpers
{
    public interface IAnnounceStatusCheck
    {
         Task<bool> CheckAnnounceStatus(AnnounceForReturnDto announce);
         Task<Role> CheckPublicRole(IRoleDal roleDal,IRoleCategoryDal roleCategoryDal);
    }
}