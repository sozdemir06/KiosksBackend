using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Entities;
using Core.Entities.Concrete;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<UserRoleForListDto>> GetUserRoles(User user);
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<Pagination<UserForListDto>> GetUserForList(UserQueryParams userQueryParams);
        Task<UserForListDto> GetUserAsync(string email);
        Task<UserForListDto> Update(UserForRegisterDto userForRegisterDto);
    }
}