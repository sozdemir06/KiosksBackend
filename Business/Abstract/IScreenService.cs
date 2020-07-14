using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IScreenService
    {
        Task<List<ScreenForReturnDto>> GetListAsync();
        Task<ScreenForReturnDto> Create(ScreenForCreationDto createDto);
        Task<ScreenForReturnDto> Update(ScreenForCreationDto updateDto);
        Task<ScreenForReturnDto> Delete(int Id);
    }
}