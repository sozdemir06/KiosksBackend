using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleFuelTypeService
    {
        Task<List<VehicleFuelTypeForReturnDto>> GetListAsync();
        Task<VehicleFuelTypeForReturnDto> Create(VehicleFuelTypeForCreationDto createDto);
        Task<VehicleFuelTypeForReturnDto> Update(VehicleFuelTypeForCreationDto updateDto);
        Task<VehicleFuelTypeForReturnDto> Delete(int Id);
    }
}