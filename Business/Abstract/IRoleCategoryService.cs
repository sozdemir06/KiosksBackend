using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IRoleCategoryService
    {
         Task<List<RoleCategoryForListDto>> GetRoleCategoriesAsync();
         Task<RoleCategoryForListDto> Create(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto);
         Task<RoleCategoryForListDto> Update(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto);
    }
}