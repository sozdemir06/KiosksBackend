using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<UserRoleForListDto>> GetUserRoles(User user);
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}