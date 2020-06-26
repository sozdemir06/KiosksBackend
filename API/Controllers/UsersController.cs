using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Helpers;
using Core.Entities;
using Core.QueryParams;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserroleService userroleService;
        public UsersController(IUserService userService, IUserroleService userroleService)
        {
            this.userroleService = userroleService;
            this.userService = userService;

        }


        [HttpGet]
        public async Task<ActionResult<Pagination<UserForListDto>>> GetUserList([FromQuery] UserQueryParams userQueryParams)
        {
            return await userService.GetUserForList(userQueryParams);
        }

        [HttpPut]
        public async Task<ActionResult<UserForListDto>> Update(UserForRegisterDto userForRegisterDto)
        {
            return await userService.Update(userForRegisterDto);
        }

        [HttpGet("roles/{userId}")]
        public async Task<ActionResult<List<UserRoleForListDto>>> GetUserRoles(int userId)
        {
            return await userroleService.GetUserRoles(userId);
        }

        [HttpPost("{userId}/addrole/{roleId}")]
        public async Task<ActionResult<RoleForListDto>> AddRoleToUser(int userId,int roleId)
        {
            return await userroleService.AddRoleToUser(userId,roleId);
        }

        [HttpDelete("{userId}/delete/{roleId}")]
        public async Task<ActionResult<RoleForListDto>> DeleteRoleFromUser(int userId,int roleId)
        {
            return await userroleService.DeleteRoleFromUser(userId,roleId);
        }

    }
}