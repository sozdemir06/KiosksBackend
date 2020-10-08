using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface INotifyGroupService
    {
        Task<List<NotifyGroupForReturnDto>> GetListAsync();
        Task<NotifyGroupForReturnDto> Create(NotifyGroupForCreationDto createDto);
        Task<NotifyGroupForReturnDto> Update(NotifyGroupForCreationDto updateDto);
        Task<NotifyGroupForReturnDto> Delete(int Id);
    }
}