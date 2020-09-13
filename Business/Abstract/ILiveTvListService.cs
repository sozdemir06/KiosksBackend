using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ILiveTvListService
    {
        Task<List<LiveTvListForReturnDto>> GetListAsync();
        Task<LiveTvListForReturnDto> Create(LiveTvListForCreationDto createDto);
        Task<LiveTvListForReturnDto> Update(LiveTvListForCreationDto updateDto);
        Task<LiveTvListForReturnDto> Delete(int Id);
    }
}