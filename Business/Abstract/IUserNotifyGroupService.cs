using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserNotifyGroupService
    {
        Task<List<UserNotifyGroupForReturnDto>> GetListAsync();
        Task<List<UserNotifyGroupForReturnDto>> GetListByUserId(int userId);
        Task<UserNotifyGroupForReturnDto> Create(UserNotifyGroupForCreationDto createDto);
        Task<UserNotifyGroupForReturnDto> Update(UserNotifyGroupForCreationDto updateDto);
        Task<UserNotifyGroupForReturnDto> Delete(int Id);
    }
}