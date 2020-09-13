using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ILiveTvBroadCastService
    {
        Task<Pagination<LiveTvBroadCastForReturnDto>> GetListAsync(LiveTvBroadCastParams queryParams);
        Task<LiveTvBroadCastForDetailDto> GetDetailAsync(int announceId);
        Task<LiveTvBroadCastForReturnDto> Create(LiveTvBroadCastForCreationDto creationDto);
        Task<LiveTvBroadCastForReturnDto> Update(LiveTvBroadCastForCreationDto updateDto);
        Task<LiveTvBroadCastForReturnDto> Publish(LiveTvBroadCastForCreationDto updateDto);
        Task<LiveTvBroadCastForReturnDto> Delete(int Id);
    }
}