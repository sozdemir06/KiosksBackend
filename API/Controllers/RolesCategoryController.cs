using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesCategoryController : ControllerBase
    {
        private readonly IRoleCategoryService roleCategoryService;
        public RolesCategoryController(IRoleCategoryService roleCategoryService)
        {
            this.roleCategoryService = roleCategoryService;

        }

        [HttpGet]
        public async Task<ActionResult<List<RoleCategoryForListDto>>> List()
        {
            return await roleCategoryService.GetRoleCategoriesAsync();
        }

        [HttpPost]
        public async Task<ActionResult<RoleCategoryForListDto>> Create(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto)
        {
            return await roleCategoryService.Create(roleCategoryForCreationAndUpdateDto);
        }

        [HttpPut]
        public async Task<ActionResult<RoleCategoryForListDto>> Update(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto)
        {
            return await roleCategoryService.Update(roleCategoryForCreationAndUpdateDto);
        }
    }
}