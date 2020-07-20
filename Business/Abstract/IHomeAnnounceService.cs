using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHomeAnnounceService
    {
         Task<Pagination<HomeAnnounceForReturnDto>> GetListAsync(HomeAnnounceParams queryParams);
         Task<HomeAnnounceForReturnDto> Create(HomeAnnounceForCreationDto creationDto);
         Task<HomeAnnounceForReturnDto> Update(HomeAnnounceForCreationDto updateDto);
         Task<HomeAnnounceForReturnDto> Delete(int Id);
    }
}