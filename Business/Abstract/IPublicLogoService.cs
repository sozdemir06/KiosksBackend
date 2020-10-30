using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPublicLogoService
    {
         Task<List<PublicLogoForReturnDto>> GetListAsync(int announceId);
         Task<PublicLogoForReturnDto> GetMainLogo();
         Task<PublicLogoForReturnDto> Create(FileUploadDto uploadDto);
         Task<PublicLogoForReturnDto> Update(PublicLogoForCreationDto updateDto);
         Task<PublicLogoForReturnDto> SetMain(PublicLogoForCreationDto updateDto);
         Task<PublicLogoForReturnDto> Delete(int Id); 
    }
}