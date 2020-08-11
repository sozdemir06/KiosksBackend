using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAnnounceService
    {
        Task<Pagination<AnnounceForReturnDto>> GetListAsync(AnnounceParams queryParams);
        Task<AnnounceForDetailDto> GetDetailAsync(int announceId);
        Task<AnnounceForReturnDto> Create(AnnounceForCreationDto creationDto);
        Task<AnnounceForReturnDto> Update(AnnounceForCreationDto updateDto);
        Task<AnnounceForReturnDto> Publish(AnnounceForCreationDto updateDto);
        Task<AnnounceForReturnDto> Delete(int Id);
    }
}