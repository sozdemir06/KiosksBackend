using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleGearTypesController : ControllerBase
    {
        private readonly IVehicleGearTypeService vehicleGearTypeService;
        public VehicleGearTypesController(IVehicleGearTypeService vehicleGearTypeService)
        {
            this.vehicleGearTypeService = vehicleGearTypeService;

        }

         [HttpGet]
        public async Task<ActionResult<List<VehicleGearTypeForReturnDto>>> List()
        {
            return await vehicleGearTypeService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<VehicleGearTypeForReturnDto>> Create(VehicleGearTypeForCreationDto createDto)
        {
            return await vehicleGearTypeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleGearTypeForReturnDto>> Update(VehicleGearTypeForCreationDto updateDto)
        {
            return await vehicleGearTypeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<VehicleGearTypeForReturnDto>> Delete(int itemId)
        {
            return await vehicleGearTypeService.Delete(itemId);
        }

    }
}