using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ISubScreenService
    {
        Task<List<SubScreenForReturnDto>> GetListAsync();
        Task<List<SubScreenForReturnDto>> GetByScreenId(int screenId);
        Task<SubScreenForReturnDto> Create(SubScreenForCreationDto createDto);
        Task<SubScreenForReturnDto> Update(SubScreenForCreationDto updateDto);
        Task<SubScreenForReturnDto> Delete(int Id);
    }
}