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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<RoleForListDto>>> List([FromQuery]RoleQueryParams queryParams)
        {
            return await roleService.GetRolesAsync(queryParams);
        }

        [HttpPost]
        public async Task<ActionResult<RoleForListDto>> Create(RoleForCreationAndUpdateDto roleForCreationAndUpdateDto)
        {
            return await roleService.Create(roleForCreationAndUpdateDto);
        }

        [HttpPut]
        public async Task<ActionResult<RoleForListDto>> Update(RoleForCreationAndUpdateDto roleForCreationAndUpdate)
        {
            return await roleService.Update(roleForCreationAndUpdate);
        }
    }
}