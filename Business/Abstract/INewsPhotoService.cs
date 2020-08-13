using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface INewsPhotoService
    {
        Task<List<NewsPhotoForReturnDto>> GetListAsync(int announceId);
        Task<NewsPhotoForReturnDto> Create(FileUploadDto uploadDto);
        Task<NewsPhotoForReturnDto> Update(NewsPhotoForCreationDto updateDto);
        Task<NewsPhotoForReturnDto> Delete(int Id);
    }
}