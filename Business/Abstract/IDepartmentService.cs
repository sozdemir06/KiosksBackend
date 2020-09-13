using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDepartmentService
    {
        Task<List<DepartmentForReturnDto>> GetDepartmentListAsync();
        Task<DepartmentForReturnDto> Create(DepartmentForCreationDto createDto);
        Task<DepartmentForReturnDto> Update(DepartmentForCreationDto updateDto);
        Task<DepartmentForReturnDto> Delete(int Id);
    }
}