using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleAnnounceService
    {
        Task<Pagination<VehicleAnnounceForReturnDto>> GetListAsync(VehicleAnnounceParams queryParams);
        Task<VehicleAnnounceForReturnDto> Create(VehicleAnnounceForCreationDto creationDto);
        Task<VehicleAnnounceForUserDto> CreateForPublicAsync(VehicleAnnounceForCreationDto creationDto,int userId);
        Task<VehicleAnnounceForReturnDto> Update(VehicleAnnounceForCreationDto updateDto);
        Task<VehicleAnnounceForUserDto> UpdateForPublicAsync(VehicleAnnounceForCreationDto updateDto,int userId);
        Task<VehicleAnnounceForReturnDto> Publish(VehicleAnnounceForCreationDto updateDto);
        Task<VehicleAnnounceForReturnDto> Delete(int Id);
    }
}