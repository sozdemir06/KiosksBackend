using System.Threading.Tasks;
using Business.Abstract;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelsController : ControllerBase
    {
        private readonly IVehicleModelService vehicleModelService;
        public VehicleModelsController(IVehicleModelService vehicleModelService)
        {
            this.vehicleModelService = vehicleModelService;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<VehicleModelForReturnDto>>> List([FromQuery]VehicleModelParams queryParams)
        {
            return await vehicleModelService.GetListAsync(queryParams);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleModelForReturnDto>> Create(VehicleModelForCreationDto createDto)
        {
            return await vehicleModelService.Create(createDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<VehicleModelForReturnDto>> Delete(int itemId)
        {
            return await vehicleModelService.Delete(itemId);
        }

        [HttpPut]
        public async Task<ActionResult<VehicleModelForReturnDto>> Update(VehicleModelForCreationDto updateDto)
        {
            return  await vehicleModelService.Update(updateDto);
        }
    }
}