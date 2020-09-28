using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAnnouncePhotoService
    {
        Task<List<AnnouncePhotoForReturnDto>> GetListAsync(int announceId);
        Task<AnnouncePhotoForReturnDto> Create(FileUploadDto uploadDto);
        Task<AnnouncePhotoForReturnDto> CreateForPublic(FileUploadDto uploadDto);
        Task<AnnouncePhotoForReturnDto> Update(AnnouncePhotoForCretionDto updateDto);
        Task<AnnouncePhotoForReturnDto> Delete(int Id);
    }
}