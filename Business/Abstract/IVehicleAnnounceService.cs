using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleAnnounceService
    {
        Task<Pagination<VehicleAnnounceForReturnDto>> GetListAsync(VehicleAnnounceParams queryParams);
        Task<VehicleAnnounceForDetailDto> GetDetailAsync(int vehicleAnnounceId);
        Task<VehicleAnnounceForReturnDto> Create(VehicleAnnounceForCreationDto creationDto);
        Task<VehicleAnnounceForReturnDto> Update(VehicleAnnounceForCreationDto updateDto);
        Task<VehicleAnnounceForReturnDto> Publish(VehicleAnnounceForCreationDto updateDto);
        Task<VehicleAnnounceForReturnDto> Delete(int Id);
    }
}