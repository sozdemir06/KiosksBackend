using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICampusService
    {
        Task<List<CampusForReturnDto>> GetCampusListAsync();
        Task<CampusForReturnDto> Create(CampuseForCreationDto createDto);
        Task<CampusForReturnDto> Update(CampuseForCreationDto updateDto);
        Task<CampusForReturnDto> Delete(int Id);
    }
}