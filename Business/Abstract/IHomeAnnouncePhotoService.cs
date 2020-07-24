using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHomeAnnouncePhotoService
    {
         Task<List<HomeAnnouncePhotoForReturnDto>> GetListAsync(int announceId);
         Task<HomeAnnouncePhotoForReturnDto> Create(FileUploadDto uploadDto);
         Task<HomeAnnouncePhotoForReturnDto> Update(HomeAnnouncePhotoForCreationDto updateDto);
         Task<HomeAnnouncePhotoForReturnDto> Delete(int Id);
    }
}