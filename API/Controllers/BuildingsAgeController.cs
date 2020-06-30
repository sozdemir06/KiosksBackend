using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsAgeController : ControllerBase
    {
        private readonly IBuildingageService buildingAgeService;
        public BuildingsAgeController(IBuildingageService buildingAgeService)
        {
            this.buildingAgeService = buildingAgeService;

        }


        [HttpGet]
        public async Task<ActionResult<List<BuildingAgeForReturnDto>>> List()
        {
            return await buildingAgeService.GetListAsync();
        }
    }
}