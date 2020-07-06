using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleModelService
    {
        Task<Pagination<VehicleModelForReturnDto>> GetListAsync(VehicleModelParams vehicleModelParams);
        Task<VehicleModelForReturnDto> Create(VehicleModelForCreationDto createDto);
        Task<VehicleModelForReturnDto> Update(VehicleModelForCreationDto updateDto);
        Task<VehicleModelForReturnDto> Delete(int Id);
    }
}