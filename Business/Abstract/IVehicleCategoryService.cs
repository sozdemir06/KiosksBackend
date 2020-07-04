using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleCategoryService
    {
        Task<List<VehicleCategoryForReturnDto>> GetListAsync();
        Task<VehicleCategoryForReturnDto> Create(VehicleCategoryForCreationDto createDto);
        Task<VehicleCategoryForReturnDto> Update(VehicleCategoryForCreationDto updateDto);
        Task<VehicleCategoryForReturnDto> Delete(int Id);
    }
}