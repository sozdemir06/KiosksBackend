using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;

        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentForReturnDto>>> List()
        {
            return await departmentService.GetDepartmentListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentForReturnDto>> Create(DepartmentForCreationDto createDto)
        {
            return await departmentService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<DepartmentForReturnDto>> Update(DepartmentForCreationDto updateDto)
        {
            return await departmentService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<DepartmentForReturnDto>> Delete(int itemId)
        {
            return await departmentService.Delete(itemId);
        }
    }
}