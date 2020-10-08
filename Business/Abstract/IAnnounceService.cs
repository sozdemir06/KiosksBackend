using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAnnounceService
    {
        Task<Pagination<AnnounceForReturnDto>> GetListAsync(AnnounceParams queryParams);
        Task<AnnounceForReturnDto> Create(AnnounceForCreationDto creationDto);
        Task<AnnounceForUserDto> CreateForPublicAsync(AnnounceForCreationDto creationDto,int userId);
        Task<AnnounceForReturnDto> Update(AnnounceForCreationDto updateDto);
        Task<AnnounceForUserDto> UpdateForPublicAsync(AnnounceForCreationDto updateDto,int userId);
        Task<AnnounceForReturnDto> Publish(AnnounceForCreationDto updateDto);
        Task<AnnounceForReturnDto> Delete(int Id);
    }
}