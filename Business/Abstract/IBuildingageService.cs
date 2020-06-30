using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IBuildingageService
    {
        Task<List<BuildingAgeForReturnDto>> GetListAsync();
        Task<BuildingAgeForReturnDto> Create(BuildingAgeForCretationDto createDto);
        Task<BuildingAgeForReturnDto> Update(BuildingAgeForCretationDto updateDto);
        Task<BuildingAgeForReturnDto> Delete(int Id);
    }
}