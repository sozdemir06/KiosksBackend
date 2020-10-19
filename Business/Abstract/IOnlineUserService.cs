using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IOnlineUserService
    {
         Task<OnlineUserForReturnDto> GetOnlineUserByIdAsync(int userId);
         Task<string> GetUserConnectionStringAsync();
         Task<OnlineUserForReturnDto> AddNewOnlineUserAsync(OnlineUser onlineUser);
         Task<List<OnlineUserForReturnDto>> GetAllOnlineUserAsync();
         Task RemoveByUserIdAsync(int userId);
    }
}