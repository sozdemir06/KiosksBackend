using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IScreenHeaderPhotoService
    {
         Task<List<ScreenHeaderPhotoForReturnDto>> GetListAsync(int announceId);
         Task<ScreenHeaderPhotoForReturnDto> Create(FileUploadDto uploadDto);
         Task<ScreenHeaderPhotoForReturnDto> Update(ScreenHeaderPhotoForCreationDto updateDto);
         Task<ScreenHeaderPhotoForReturnDto> SetMain(ScreenHeaderPhotoForCreationDto updateDto);
         Task<ScreenHeaderPhotoForReturnDto> Delete(int Id); 
    }
}