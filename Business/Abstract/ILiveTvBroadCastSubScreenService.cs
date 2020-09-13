using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ILiveTvBroadCastSubScreenService
    {
         Task<List<LiveTvBroadCastSubScreenForReturnDto>> GetListAsync();
         Task<List<LiveTvBroadCastSubScreenForReturnDto>> GetByAnnounceId(int announceId);
         Task<LiveTvBroadCastSubScreenForReturnDto> Create(LiveTvBroadCastSubScreenForCreationDto creationDto);
         Task<LiveTvBroadCastSubScreenForReturnDto> Update(LiveTvBroadCastSubScreenForCreationDto updateDto);
         Task<LiveTvBroadCastSubScreenForReturnDto> Delete(int Id);
    }
}