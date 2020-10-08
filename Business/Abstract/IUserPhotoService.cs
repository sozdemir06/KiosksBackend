using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserPhotoService
    {
        Task<List<UserPhotoForReturnDto>> GetListAsync(int announceId);
        Task<UserPhotoForReturnDto> GetMain();
        Task<UserPhotoForReturnDto> Create(FileUploadDto uploadDto);
        Task<UserPhotoForReturnDto> Update(UserPhotoForCreationDto updateDto);
        Task<UserPhotoForReturnDto> Delete(int Id);
    }
}