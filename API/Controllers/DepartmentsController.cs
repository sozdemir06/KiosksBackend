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
        public async Task<ActionResult<List<DepartmentForListDto>>> List()
        {
            return await departmentService.GetDepartmentListAsync();
        }
    }
}