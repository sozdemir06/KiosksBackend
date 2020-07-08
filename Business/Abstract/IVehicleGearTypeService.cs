using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleGearTypeService
    {
        Task<List<VehicleGearTypeForReturnDto>> GetListAsync();
        Task<VehicleGearTypeForReturnDto> Create(VehicleGearTypeForCreationDto createDto);
        Task<VehicleGearTypeForReturnDto> Update(VehicleGearTypeForCreationDto updateDto);
        Task<VehicleGearTypeForReturnDto> Delete(int Id);
    }
}