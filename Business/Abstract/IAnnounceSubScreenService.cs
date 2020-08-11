using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAnnounceSubScreenService
    {
        Task<List<AnnounceSubScreenForReturnDto>> GetListAsync();
        Task<List<AnnounceSubScreenForReturnDto>> GetByAnnounceId(int announceId);
        Task<AnnounceSubScreenForReturnDto> Create(AnnounceSubScreenForCreationDto creationDto);
        Task<AnnounceSubScreenForReturnDto> Update(AnnounceSubScreenForCreationDto updateDto);
        Task<AnnounceSubScreenForReturnDto> Delete(int Id);
    }
}