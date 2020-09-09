using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICityService
    {
        Task<List<CityForReturnDto>> GetListAsync();
        Task<CityForReturnDto> Create(CityForCreationDto createDto);
        Task<CityForReturnDto> Update(CityForCreationDto updateDto);
        Task<CityForReturnDto> Delete(int Id);
    }
}