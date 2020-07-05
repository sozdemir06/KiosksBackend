using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleBrandService
    {
        Task<Pagination<VehicleBrandForReturnDto>> GetListAsync(VehicleBrandParams vehicleBrandParams);
        Task<VehicleBrandForReturnDto> Create(VehicleBrandForCreationDto createDto);
        Task<VehicleBrandForReturnDto> Update(VehicleBrandForCreationDto updateDto);
        Task<VehicleBrandForReturnDto> Delete(int Id);
    }
}