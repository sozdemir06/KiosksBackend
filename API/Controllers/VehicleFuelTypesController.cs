using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleFuelTypesController : ControllerBase
    {
        private readonly IVehicleFuelTypeService vehicleFuelTypeService;

        public VehicleFuelTypesController(IVehicleFuelTypeService vehicleFuelTypeService)
        {
            this.vehicleFuelTypeService = vehicleFuelTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleFuelTypeForReturnDto>>> List()
        {
            return await vehicleFuelTypeService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<VehicleFuelTypeForReturnDto>> Create(VehicleFuelTypeForCreationDto createDto)
        {
            return await vehicleFuelTypeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleFuelTypeForReturnDto>> Update(VehicleFuelTypeForCreationDto updateDto)
        {
            return await vehicleFuelTypeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<VehicleFuelTypeForReturnDto>> Delete(int itemId)
        {
            return await vehicleFuelTypeService.Delete(itemId);
        }

    }
}