using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface INewsSubScreenService
    {
        Task<List<NewsSubScreenForReturnDto>> GetListAsync();
        Task<List<NewsSubScreenForReturnDto>> GetByAnnounceId(int announceId);
        Task<NewsSubScreenForReturnDto> Create(NewsSubScreenForCreationDto creationDto);
        Task<NewsSubScreenForReturnDto> Update(NewsSubScreenForCreationDto updateDto);
        Task<NewsSubScreenForReturnDto> Delete(int Id);
    }
}