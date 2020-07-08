using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleEngineSizeService
    {
        Task<List<VehicleEngineSizeForReturnDto>> GetListAsync();
        Task<VehicleEngineSizeForReturnDto> Create(VehicleEngineSizeForCreationDto createDto);
        Task<VehicleEngineSizeForReturnDto> Update(VehicleEngineSizeForCreationDto updateDto);
        Task<VehicleEngineSizeForReturnDto> Delete(int Id);
    }
}