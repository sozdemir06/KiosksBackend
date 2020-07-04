using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHeatingTypeService
    {
        Task<List<HeatingTypeForReturnDto>> GetListAsync();
        Task<HeatingTypeForReturnDto> Create(HeatingTypeForCreationDto createDto);
        Task<HeatingTypeForReturnDto> Update(HeatingTypeForCreationDto updateDto);
        Task<HeatingTypeForReturnDto> Delete(int Id);
    }
}