using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreesController : ControllerBase
    {
        private readonly IDegreeService degreeService;
        public DegreesController(IDegreeService degreeService)
        {
            this.degreeService = degreeService;

        }

        [HttpGet]
        public async Task<ActionResult<List<DegreeForListDto>>> List()
        {
            return await degreeService.GetDegreeListAsync();
        }
    }
}