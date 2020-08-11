using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAnnounceContentTypeService
    {
        Task<List<AnnounceContentTypeForReturnDto>> GetListAsync();
        Task<AnnounceContentTypeForReturnDto> Create(AnnounceContentTypeForCreationDto createDto);
        Task<AnnounceContentTypeForReturnDto> Update(AnnounceContentTypeForCreationDto updateDto);
        Task<AnnounceContentTypeForReturnDto> Delete(int Id);
    }
}