using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Helpers;
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
        public UsersController(IUserService userService)
        {
            this.userService = userService;

        }


        [HttpGet]
        public async Task<Pagination<UserForListDto>> GetUserList([FromQuery]UserQueryParams userQueryParams)
        {
            return await userService.GetUserForList(userQueryParams);
        }
    }
}