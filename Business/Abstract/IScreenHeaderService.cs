using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IScreenHeaderService
    {
        Task<List<ScreenHeaderForReturnDto>> GetListAsync();
        Task<ScreenHeaderForReturnDto> Create(ScreenHeaderForCreationDto createDto);
        Task<ScreenHeaderForReturnDto> Update(ScreenHeaderForCreationDto updateDto);
        Task<ScreenHeaderForReturnDto> Delete(int Id);
    }
}