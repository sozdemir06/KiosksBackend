using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusesController : ControllerBase
    {
        private readonly ICampusService campuseService;
        public CampusesController(ICampusService campuseService)
        {
            this.campuseService = campuseService;

        }


        [HttpGet]
        public async Task<ActionResult<List<CampusForListDto>>> List()
        {
            return await campuseService.GetCampusListAsync();
        }
    }
}