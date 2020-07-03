using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IFlatOfHomeService
    {
        Task<List<FlatOfHomeForReturnDto>> GetListAsync();
        Task<FlatOfHomeForReturnDto> Create(FlatOfHomeForCreationDto createDto);
        Task<FlatOfHomeForReturnDto> Update(FlatOfHomeForCreationDto updateDto);
        Task<FlatOfHomeForReturnDto> Delete(int Id);
    }
}