using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDepartmentService
    {
         Task<List<DepartmentForListDto>> GetDepartmentListAsync();
    }
}