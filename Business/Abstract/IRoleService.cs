using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IRoleService
    {
         Task<Pagination<RoleForListDto>> GetRolesAsync(RoleQueryParams queryParams);
         Task<RoleForListDto> Create(RoleForCreationAndUpdateDto roleForCreationAndUpdateDto);
         Task<RoleForListDto> Update(RoleForCreationAndUpdateDto roleForCreationAndUpdateDto);
    }
}