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
        public async Task<ActionResult<List<CampusForReturnDto>>> List()
        {
            return await campuseService.GetCampusListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CampusForReturnDto>> Create(CampuseForCreationDto createDto)
        {
            return await campuseService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<CampusForReturnDto>> Update(CampuseForCreationDto updateDto)
        {
            return await campuseService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<CampusForReturnDto>> Delete(int itemId)
        {
            return await campuseService.Delete(itemId);
        }
    }
}