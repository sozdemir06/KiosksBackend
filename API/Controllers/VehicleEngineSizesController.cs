using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleEngineSizesController : ControllerBase
    {
        private readonly IVehicleEngineSizeService vehcileEngineSizeService;
        public VehicleEngineSizesController(IVehicleEngineSizeService vehcileEngineSizeService)
        {
            this.vehcileEngineSizeService = vehcileEngineSizeService;

        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleEngineSizeForReturnDto>>> List()
        {
            return await vehcileEngineSizeService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<VehicleEngineSizeForReturnDto>> Create(VehicleEngineSizeForCreationDto createDto)
        {
            return await vehcileEngineSizeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleEngineSizeForReturnDto>> Update(VehicleEngineSizeForCreationDto updateDto)
        {
            return await vehcileEngineSizeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<VehicleEngineSizeForReturnDto>> Delete(int itemId)
        {
            return await vehcileEngineSizeService.Delete(itemId);
        }



    }
}