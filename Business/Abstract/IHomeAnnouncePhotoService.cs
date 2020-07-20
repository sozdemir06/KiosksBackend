using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHomeAnnouncePhotoService
    {
         Task<List<HomeAnnouncePhotoForReturnDto>> GetListAsync();
         Task<HomeAnnouncePhotoForReturnDto> Create(HomeAnnouncePhotoForCreationDto creationDto);
         Task<HomeAnnouncePhotoForReturnDto> Update(HomeAnnouncePhotoForCreationDto updateDto);
         Task<HomeAnnouncePhotoForReturnDto> Delete(int Id);
    }
}