using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IVehicleAnnounceSubScreenService
    {
         Task<List<VehicleAnnounceSubScreenForReturnDto>> GetListAsync();
         Task<List<VehicleAnnounceSubScreenForReturnDto>> GetByAnnounceId(int announceId);
         Task<VehicleAnnounceSubScreenForReturnDto> Create(VehicleAnnounceSubScreenForCreationDto creationDto);
         Task<VehicleAnnounceSubScreenForReturnDto> Update(VehicleAnnounceSubScreenForCreationDto updateDto);
         Task<VehicleAnnounceSubScreenForReturnDto> Delete(int Id);
    }
}