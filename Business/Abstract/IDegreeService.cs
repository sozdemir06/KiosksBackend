using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDegreeService
    {
        Task<Pagination<DegreeForReturnDto>> GetListAsync(DegreeParams queryParams);
        Task<List<DegreeForReturnDto>> GetListWithoutPaging(int categoryId);
        Task<DegreeForReturnDto> Create(DegreeForCreationDto createDto);
        Task<DegreeForReturnDto> Update(DegreeForCreationDto updateDto);
        Task<DegreeForReturnDto> Delete(int Id);
    }
}