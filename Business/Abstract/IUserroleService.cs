using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserroleService
    {
         Task<List<UserRoleForListDto>> GetUserRoles(int userId);
         Task<RoleForListDto> AddRoleToUser(int userId,int roleId);
         Task<RoleForListDto> DeleteRoleFromUser(int roleId,int userId);
    }
}