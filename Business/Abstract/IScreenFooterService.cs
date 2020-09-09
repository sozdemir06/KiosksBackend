using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IScreenFooterService
    {
        Task<List<ScreenFooterForReturnDto>> GetListAsync();
        Task<ScreenFooterForReturnDto> Create(ScreenFooterForCreationDto createDto);
        Task<ScreenFooterForReturnDto> Update(ScreenFooterForCreationDto updateDto);
        Task<ScreenFooterForReturnDto> Delete(int Id);  
    }
}