using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IOnlineScreenService
    {
         Task<OnlineScreenForReturnDto> GetOnlineScreenByIdAsync(int screenId);
         Task<string> GetScreenConnectionStringAsync(int screenId);
         Task<string> GetScreenByConnectionIdAsync( string connectionId);
         Task<OnlineScreenForReturnDto> AddNewOnlineScreenAsync(OnlineScreen onlineScreen);
         Task<List<OnlineScreenForReturnDto>> GetAllOnlineScreenAsync();
         Task<string[]> GetAllOnlineScreenConnectionId();
         Task<string[]> GetOnlineScreenConnectionIdByScreenId(int screenId);
         Task RemoveByScreenIdAsync(int screenId); 
         Task RemoveByConnectionIdAsync(string connectionId); 
    }
}