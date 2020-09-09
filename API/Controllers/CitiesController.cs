using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;
        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;

        }

        [HttpGet]
        public async Task<ActionResult<List<CityForReturnDto>>> List()
        {
            return await cityService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CityForReturnDto>> Create(CityForCreationDto createDto)
        {
            return await cityService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<CityForReturnDto>> Update(CityForCreationDto updateDto)
        {
            return await cityService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<CityForReturnDto>> Delete(int itemId)
        {
            return await cityService.Delete(itemId);
        }
    }
}