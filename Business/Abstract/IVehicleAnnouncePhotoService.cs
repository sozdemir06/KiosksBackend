using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleAnnouncePhotoService
    {
        Task<List<VehicleAnnouncePhotoForReturnDto>> GetListAsync(int announceId);
        Task<VehicleAnnouncePhotoForReturnDto> Create(FileUploadDto uploadDto);
        Task<VehicleAnnouncePhotoForReturnDto> Update(VehicleAnnouncePhotoForCreationDto updateDto);
        Task<VehicleAnnouncePhotoForReturnDto> Delete(int Id);
    }
}