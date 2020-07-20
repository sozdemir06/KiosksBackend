using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHomeAnnounceSubScreenService
    {
         Task<List<HomeAnnounceSubScreenForReturnDto>> GetListAsync();
         Task<HomeAnnounceSubScreenForReturnDto> Create(HomeAnnounceSubScreenForCreationDto creationDto);
         Task<HomeAnnounceSubScreenForReturnDto> Update(HomeAnnounceSubScreenForCreationDto updateDto);
         Task<HomeAnnounceSubScreenForReturnDto> Delete(int Id);
    }
}