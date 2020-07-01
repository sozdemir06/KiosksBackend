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

        [HttpPost]
        public async Task<ActionResult<BuildingAgeForReturnDto>> Create(BuildingAgeForCretationDto createDto)
        {
            return await buildingAgeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<BuildingAgeForReturnDto>> Update(BuildingAgeForCretationDto updateDto)
        {
            return await buildingAgeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<BuildingAgeForReturnDto>> Delete(int itemId)
        {
            return await buildingAgeService.Delete(itemId);
        }

        
    }
}