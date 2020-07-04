using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeatingTypesController : ControllerBase
    {
        private readonly IHeatingTypeService heatingTypeService;
        public HeatingTypesController(IHeatingTypeService heatingTypeService)
        {
            this.heatingTypeService = heatingTypeService;

        }

         [HttpGet]
        public async Task<ActionResult<List<HeatingTypeForReturnDto>>> List()
        {
            return await heatingTypeService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<HeatingTypeForReturnDto>> Create(HeatingTypeForCreationDto createDto)
        {
            return await heatingTypeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<HeatingTypeForReturnDto>> Update(HeatingTypeForCreationDto updateDto)
        {
            return await heatingTypeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<HeatingTypeForReturnDto>> Delete(int itemId)
        {
            return await heatingTypeService.Delete(itemId);
        }

    }
}