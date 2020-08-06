using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleModelService
    {
        Task<Pagination<VehicleModelForReturnDto>> GetListAsync(VehicleModelParams vehicleModelParams);
        Task<List<VehicleModelForReturnDto>> GetListByBrandIdAsync(int brandId);
        Task<VehicleModelForReturnDto> Create(VehicleModelForCreationDto createDto);
        Task<VehicleModelForReturnDto> Update(VehicleModelForCreationDto updateDto);
        Task<VehicleModelForReturnDto> Delete(int Id);
    }
}